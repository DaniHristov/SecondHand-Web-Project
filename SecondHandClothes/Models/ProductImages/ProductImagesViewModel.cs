namespace SecondHandClothes.Models.ProductImages
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using SecondHandClothes.Data.Models;

    public class ProductImagesViewModel
    {
        public IList<IFormFile> Images { get; set; }

        public Product Product { get; set; }
    }
}
