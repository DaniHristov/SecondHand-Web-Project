using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandClothes.Services.Products.Models
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Description { get; init; }

        public string SellerName { get; init; }

        public int SellerId { get; init; }

        public string UserId { get; init; }

        public string Category { get; init; }

        public string Colour { get; init; }

        public string CreatedOn { get; init; }
    }
}
