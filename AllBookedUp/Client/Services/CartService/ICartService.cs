using AllBookedUp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Product product, string user);
        Task<List<CartItem>> GetCartItems(string user);
        Task DeleteItem(CartItem item);
    }
}
