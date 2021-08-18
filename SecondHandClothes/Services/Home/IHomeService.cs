namespace SecondHandClothes.Services.Home
{
    using System.Collections.Generic;

    public interface IHomeService
    {
        IList<IndexQueryServiceModel> GetIndexProducts();
    }
}
