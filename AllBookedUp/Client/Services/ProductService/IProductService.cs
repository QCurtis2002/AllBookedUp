﻿using AllBookedUp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged; 
        List<Product> Products { get; set; }
        string Message { get; set; }
        Task GetProducts(string categoryUrl = null);
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task SearchProducts(string searchText);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
