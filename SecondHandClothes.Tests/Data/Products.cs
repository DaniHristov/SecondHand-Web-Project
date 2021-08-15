namespace SecondHandClothes.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using SecondHandClothes.Data.Models;

    public static class Products
    {
        public static IEnumerable<Product> TenPublicProducts()
            => Enumerable.Range(0, 10).Select(p => new Product
            {

            });
    }
}
