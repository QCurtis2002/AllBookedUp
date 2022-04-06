using AllBookedUp.Client.Services.ProductService;
using AllBookedUp.Shared;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Services.WishlistService
{
    public class WishlistService : IWishlistService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public WishlistService(ILocalStorageService localStorage,
            IToastService toastService,
            IProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
        }

        /// <summary>
        /// Add the item to the wishlist local storage
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddToWishlist(Product product)
        {
            var wishlist = await _localStorage.GetItemAsync<List<Product>>("wishlist");
            if (wishlist == null)
            {
                wishlist = new List<Product>();
            }
            wishlist.Add(product);
            await _localStorage.SetItemAsync("wishlist", wishlist);

            var prod = await _productService.GetProductById(product.Id);
            //below line may be wrong
            _toastService.ShowSuccess(product.Title, "Added to Wishlist:");

        }

        /// <summary>
        /// The the items that are in the wishlist
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartItem>> GetWishlistItems()
        {
            var result = new List<CartItem>();
            var wishlist = await _localStorage.GetItemAsync<List<Product>>("wishlist");
            if (wishlist == null)
            {
                return result;
            }

            foreach (var item in wishlist)
            {
                var product = await _productService.GetProductById(item.Id);
                var cartItem = new CartItem
                {
                    ProductId = product.Data.Id,
                    ProductTitle = product.Data.Title,
                    Price = product.Data.Price,
                    Image = product.Data.ImageUrl
                };

                result.Add(cartItem);
            }

            return result;

        }

        /// <summary>
        /// Delete the item from the Wishlist local storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task DeleteItem(CartItem item)
        {
            var wishlist = await _localStorage.GetItemAsync<List<Product>>("wishlist");
            if (wishlist == null)
            {
                return;
            }

            var cartItem = wishlist.Find(x => x.Id == item.ProductId);
            wishlist.Remove(cartItem);

            await _localStorage.SetItemAsync("wishlist", wishlist);
        }
    }
}
