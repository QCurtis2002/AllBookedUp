using AllBookedUp.Client.Services.ProductService;
using AllBookedUp.Shared;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage,
            IToastService toastService,
            IProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
        }

        public async Task AddToCart(Product product)
        {
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");
            if (cart == null)
            {
                cart = new List<Product>();
            }
            cart.Add(product);
            await _localStorage.SetItemAsync("cart", cart);

            var prod = await _productService.GetProductById(product.Id);
            //below line may be wrong
            _toastService.ShowSuccess(product.Title, "Added to cart:");

            OnChange.Invoke();

        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var result = new List<CartItem>();
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");
            if (cart == null)
            {
                return result;
            }

            foreach (var item in cart)
            {
                var product = await _productService.GetProductById(item.Id);
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title
                }
            }
        }
    }
}
