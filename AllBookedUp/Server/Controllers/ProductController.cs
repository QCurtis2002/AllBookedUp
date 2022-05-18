using AllBookedUp.Server.Data;
using AllBookedUp.Server.Services.ProductService;
using AllBookedUp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllBookedUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get products from the productService
        /// </summary>
        /// <returns></returns>
        [HttpGet("getproducts")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var response = await _productService.GetProducts();
            return Ok(response);
        }

        /// <summary>
        /// Get a product from the productService
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(int id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }

        /// <summary>
        /// Get a list of products belonging to category URL from productService
        /// </summary>
        /// <param name="categoryUrl"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategory(categoryUrl);
            return Ok(result);
        }

        /// <summary>
        /// Get search results from productService
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText, int page = 1)
        {
            var result = await _productService.SearchProducts(searchText, page);
            return Ok(result);
        }

        /// <summary>
        /// Get search suggestions from product service
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet("SearchSuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _productService.GetProductSearchSuggestions(searchText);
            return Ok(result);
        }

        /// <summary>
        /// Get the featured products from the product service
        /// </summary>
        /// <returns></returns>
        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
        {
            var result = await _productService.GetFeaturedProducts();
            return Ok(result);
        }

    }
}
