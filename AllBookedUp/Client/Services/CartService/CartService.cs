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

        /// <summary>
        /// Add a product to the cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddToCart(Product product, string user)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductTitle = product.Title,
                Price = product.Price,
                Image = product.ImageUrl,
                User = user
            };

            cart.Add(cartItem);
            await _localStorage.SetItemAsync("cart", cart);

            var prod = await _productService.GetProductById(product.Id);
            _toastService.ShowSuccess(product.Title, "Added to cart:");

            OnChange.Invoke();

        }

        /// <summary>
        /// Get a list of items currently in the cart local storage
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartItem>> GetCartItems(string user)
        {
            var result = new List<CartItem>();
            var wishlist = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (wishlist == null)
            {
                return result;
            }

            foreach (var item in wishlist)
            {
                if (item.User == user)
                {
                    var product = await _productService.GetProductById(item.ProductId);
                    var cartItem = new CartItem
                    {
                        ProductId = product.Data.Id,
                        ProductTitle = product.Data.Title,
                        Price = product.Data.Price,
                        Image = product.Data.ImageUrl
                    };

                    result.Add(cartItem);
                }

            }

            return result;
        }

        /// <summary>
        /// Delete a item from the cart
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task DeleteItem(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == item.ProductId);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();

        }
    }
}
