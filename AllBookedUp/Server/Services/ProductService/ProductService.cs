using AllBookedUp.Server.Data;
using AllBookedUp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Server.Services.ProductService
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get the featured products from the database
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Featured)
                .ToListAsync()
            };

            return response;
        }

        /// <summary>
        /// Get a product given the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exist.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        /// <summary>
        /// Get a list of products from the database
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var response = new ServiceResponse<List<Product>>
            {
                Data = products
            };
            return response;
        }

        /// <summary>
        /// Get a list of products by category from the database
        /// </summary>
        /// <param name="categoryUrl"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync()
            };

            return response;
        }

        /// <summary>
        /// Get Product search suggestions depending on what the title and description contains
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(w => w.Trim(punctuation));
                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };


        }

        /// <summary>
        /// Get the products that match search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);

            if(pageCount > 10)
            {
                pageCount = 10;
            }

            var products = await _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower()))
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        /// <summary>
        /// Find the products that match the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower()))
                            .ToListAsync();
        }
    }
}
