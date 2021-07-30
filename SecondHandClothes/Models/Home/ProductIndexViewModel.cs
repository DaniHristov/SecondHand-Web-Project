namespace SecondHandClothes.Models.Home
{
    using System;
    
    public class ProductIndexViewModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string Brand { get; init; }

        public string Category { get; init; }

        public string ImageUrl { get; init; }
    }
}
