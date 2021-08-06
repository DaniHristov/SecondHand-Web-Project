namespace SecondHandClothes.Services.Home
{
    using System.Collections.Generic;

    public interface IHomeService
    {
        IEnumerable<IndexQueryServiceModel> GetIndexProducts(string id, string brand, string categoryName, string imageUrl);
    }
}
