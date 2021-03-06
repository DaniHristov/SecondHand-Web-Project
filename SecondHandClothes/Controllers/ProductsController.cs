namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models.Products;
    using SecondHandClothes.Services.Products;
    using SecondHandClothes.Services.Sellers;

    using static WebConstants;
    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly ISellerService sellers;

        public ProductsController(IProductService products, ISellerService sellers, SecondHandDbContext data)
        {
            this.products = products;
            this.sellers = sellers;
        }

        public IActionResult All([FromQuery] AllProductsQueryModel model)
        {
            var products = this.products.All(
                model.Category,
                model.Manufacturer,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productBrands = this.products.AllBrands();
            var productCategories = this.products.AllCategories();

            model.TotalProducts = products.TotalProducts;
            model.Products = products.Products;
            model.Manufacturers = productBrands;
            model.Categories = productCategories;

            return View(model);

        }

        [Authorize]
        public IActionResult MyProducts()
        {
            if (!sellers.IsSeller(this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var myProducts = this.products.ProductsByUser(this.User.Id());

            return View(myProducts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            if (!sellers.IsSeller(this.User.Id()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return this.View(
            new ProductFormModel
            {
                Categories = this.products.GetCategories(),
                Sizes = this.products.GetSizes(),
                Conditions = this.products.GetConditions(),
                Sexes = this.products.GetSexes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var sellerId = sellers.SellerId(this.User.Id());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Избраната категория е невалидна!");
            }

            if (products.SexExists(product.SexId))
            {
                this.ModelState.AddModelError(nameof(product.SexId), "Избраният пол е невалиден!");
            }

            if (products.ConditionExists(product.ConditionId))
            {
                this.ModelState.AddModelError(nameof(product.ConditionId), "Избраното състояние за продукт не е валидно!");
            }

            if (products.SizeExists(product.SizeId))
            {
                this.ModelState.AddModelError(nameof(product.SizeId), "Избраният размер не е валиден!");
            }


            if (!ModelState.IsValid)
            {
                product.Categories = this.products.GetCategories();
                product.Sexes = this.products.GetSexes();
                product.Conditions = this.products.GetConditions();
                product.Sizes = this.products.GetSizes();

                return View(product);
            }

            var produtctId = this.products.Create(
                product.Title,
                product.Description,
                product.Colour,
                product.ConditionId,
                product.CategoryId,
                product.SexId,
                product.Manufacturer,
                product.Price,
                product.SizeId,
                product.ImageURL,
                sellerId);

            TempData[GlobalMessageKey] = "Продуктът ви беше добавен успешно и чака потвърждение от админ!";

            return RedirectToAction(nameof(Details), new { id = produtctId });
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var userId = this.User.Id();

            if (!sellers.IsSeller(this.User.Id()) && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }


            return this.View(
            new ProductFormModel
            {
                Categories = this.products.GetCategories(),
                Sizes = this.products.GetSizes(),
                Conditions = this.products.GetConditions(),
                Sexes = this.products.GetSexes(),
                Description = product.Description,
                CategoryId = product.CategoryId,
                Colour = product.Colour,
                ConditionId = product.ConditionId,
                ImageURL = product.ImageUrl,
                Manufacturer = product.Brand,
                Price = product.Price,
                SexId = product.SexId,
                SizeId = product.SizeId,
                Title = product.Title
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, ProductFormModel product)
        {
            var sellerId = sellers.SellerId(this.User.Id());

            if (sellerId == 0 && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (products.SexExists(product.SexId))
            {
                this.ModelState.AddModelError(nameof(product.SexId), "Sex does not exist.");
            }

            if (products.ConditionExists(product.ConditionId))
            {
                this.ModelState.AddModelError(nameof(product.ConditionId), "Condition does not exist.");
            }

            if (products.SizeExists(product.SizeId))
            {
                this.ModelState.AddModelError(nameof(product.SizeId), "Size does not exist.");
            }


            if (!ModelState.IsValid)
            {
                product.Categories = this.products.GetCategories();
                product.Sexes = this.products.GetSexes();
                product.Conditions = this.products.GetConditions();
                product.Sizes = this.products.GetSizes();

                return View(product);
            }

            if (!this.products.IsBySeller(id, sellerId) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            this.products.Edit(
                id,
                product.Title,
                product.Description,
                product.Colour,
                product.ConditionId,
                product.CategoryId,
                product.SexId,
                product.Manufacturer,
                product.Price,
                product.SizeId,
                product.ImageURL,
                this.User.IsAdmin());

            if (User.IsAdmin())
            {
                return RedirectToAction(nameof(All));
            }

            TempData[GlobalMessageKey] = $"Продуктът беше редактиран успешно{(this.User.IsAdmin() ? string.Empty : " и чака потвърждение от админ!")}!";

            return RedirectToAction(nameof(MyProducts));
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            if (!products.IsProductFound(id))
            {
                return View("NotFound");
            }

            var product = products.Details(id);

            return this.View(product);
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = products.ProductById(id);

            var sellerId = sellers.SellerId(this.User.Id());
            if (product.SellerId != sellerId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            products.Delete(id);
            return RedirectToAction(nameof(All));
        }
    }
}
