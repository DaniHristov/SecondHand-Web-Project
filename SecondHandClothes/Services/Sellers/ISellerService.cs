using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandClothes.Services.Sellers
{
    public interface ISellerService
    {
        public bool IsSeller(string userId);



        public int SellerId(string userId);
    }
}
