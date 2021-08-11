namespace SecondHandClothes.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Models;
    using SecondHandClothes.Services.Products.Models;

    public class ProductService : IProductService
    {
        private readonly SecondHandDbContext data;


        public ProductService(SecondHandDbContext data)
        {
            this.data = data;
        }

        public ProductQueryServiceModel All(
            string category,
            string manufacturer,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage)
        {

            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                productsQuery = productsQuery
                    .Where(c => c.Category.CategoryName == category);
            }

            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                productsQuery = productsQuery.Where(c =>
                     c.Manufacturer == manufacturer);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    (p.Title).ToLower().Contains(searchTerm.ToLower()) ||
                    p.Category.CategoryName.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.PriceAscending => productsQuery.OrderBy(x => x.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(x => x.Price),
                ProductSorting.CreatedOn or _ => productsQuery.OrderByDescending(x => x.CreatedOn)
            };

            var productsCount = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Brand = p.Manufacturer,
                    Condition = p.Condition.ConditionType,
                    Price = p.Price,
                    ImageUrl = p.ImageURL,
                    Title = p.Title
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = productsCount,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public string Create(
            string title, 
            string description, 
            string colour, 
            int conditionId, 
            int categoryId, 
            int sexId, 
            string manufacturer, 
            decimal price, 
            int sizeId, 
            string imageURL, 
            int sellerId)
        {

            var productData = new Product
            {
                Title = title,
                Description = description,  
                Colour = colour,
                ConditionId = conditionId,
                CategoryId = categoryId,
                SexId = sexId,
                Manufacturer = manufacturer,
                Price = price,
                SizeId = sizeId,
                ImageURL = imageURL,
                SellerId = sellerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public bool Edit(
            string id,
            string title,
            string description,
            string colour,  
            int conditionId,
            int categoryId,
            int sexId,
            string manufacturer,
            decimal price,
            int sizeId,
            string imageURL)
        {

            var product = this.data.Products.Find(id);

            if (product==null)
            {
                return false;
            }

            product.Title = title;
            product.Description = description;
            product.Colour = colour;
            product.ConditionId = conditionId;
            product.CategoryId = categoryId;
            product.SexId = sexId;
            product.Manufacturer = manufacturer;
            product.Price = price;
            product.SizeId = sizeId;
            product.ImageURL = imageURL;

            this.data.SaveChanges();

            return true;
        }

        public ProductDetailsServiceModel Details(string id)
            => this.data
            .Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDetailsServiceModel
            {
                Id = p.Id,
                Brand = p.Manufacturer,
                Description = p.Description,
                Category = p.Category.CategoryName,
                Colour = p.Colour,
                Condition = p.Condition.ConditionType,
                ImageUrl = p.ImageURL,
                Price = p.Price,
                Title = p.Title,
                SellerId = p.SellerId,
                SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
                UserId = p.Seller.UserId,
                CreatedOn = p.CreatedOn.ToString("F"),
                CategoryId = p.CategoryId,
                ConditionId = p.ConditionId,
                Sex = p.Sex.SexType,
                SexId = p.SexId,
                Size = p.Size.SizeType,
                SizeId = p.SizeId

            })
            .FirstOrDefault();


        public IEnumerable<string> AllBrands()
            => this.data.Products
                .Select(p => p.Manufacturer)
                .Distinct()
                .OrderBy(p=>p)
                .ToList();

        public IEnumerable<string> AllCategories()
            =>this.data.Products
                    .Select(p => p.Category.CategoryName)
                    .Distinct()
                    .OrderBy(x=>x)
                    .ToList();

        public IEnumerable<ProductServiceModel> ProductsByUser(string userId)
                => GetProducts(this.data
                    .Products
                    .Where(s => s.Seller.UserId == userId));

        public bool IsBySeller(string productId, int sellerId)
            => this.data
            .Products
            .Any(p => p.Id == productId 
                && p.SellerId == sellerId);

        private static IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
           => productQuery
               .Select(p => new ProductServiceModel
               {
                   Id = p.Id,
                   Brand = p.Manufacturer,
                   Condition = p.Condition.ConditionType,
                   Price = p.Price,
                   ImageUrl = p.ImageURL,
                   Title = p.Title
               })
               .ToList();

        public IEnumerable<ProductsCategoryServiceModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new ProductsCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.CategoryName
                })
                .ToList();

        public IEnumerable<ProductsSexServiceModel> GetSexes()
             => this.data
                .Sexes
                .Select(c => new ProductsSexServiceModel
                {
                    Id = c.Id,
                    Name = c.SexType
                })
                .ToList();

        public IEnumerable<ProductsConditionServiceModel> GetConditions()
             => this.data
                .Conditions
                .Select(c => new ProductsConditionServiceModel
                {
                    Id = c.Id,
                    Name = c.ConditionType
                })
                .ToList();

        public IEnumerable<ProductsSizeServiceModel> GetSizes()
             => this.data
                .Sizes
                .Select(c => new ProductsSizeServiceModel
                {
                    Id = c.Id,
                    Name = c.SizeType
                })
                .ToList();

        public bool SexExists(int sexId)
            => !this.data.Sexes.Any(s => s.Id == sexId);


        public bool SizeExists(int sizeId)
            => !this.data.Sizes.Any(s => s.Id == sizeId);


        public bool CategoryExists(int categoryId)
            => !this.data.Categories.Any(c => c.Id == categoryId);

        public bool ConditionExists(int conditionId)
             => !this.data.Conditions.Any(c => c.Id == conditionId);

        public Product ProductById(string id) 
            => this.data.Products.FirstOrDefault(p=>p.Id == id);

        public bool Delete(string productId)
        {
            var product = data.Products.Find(productId);            

            if (product == null)
            {
                return false;
            }

            data.Products.Remove(product);
            data.SaveChanges();

            return true;
        }

        public bool IsProductFound(string productId)
        {
            var product = this.data.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return false;
            }

            return true;
        }

        public Product ProductByOrderId(string orderId)
            => this.data.Products.Where(p => p.Id == orderId).FirstOrDefault();
    }
}
