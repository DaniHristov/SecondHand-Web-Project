namespace SecondHandClothes.Controllers.Api
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Models;
    using SecondHandClothes.Models.Api.Products;
    using SecondHandClothes.Services.Products;

    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsApiController(IProductService productService)
            => this.productService = productService;

        [HttpGet]
        public ProductQueryServiceModel All([FromQuery] AllProductsApiRequestModel model) 
            => this.productService.All(
                    model.Category,
                    model.Manufacturer,
                    model.SearchTerm,
                    model.Sorting,
                    model.CurrentPage,
                    model.ProductsPerPage);
    }
}
