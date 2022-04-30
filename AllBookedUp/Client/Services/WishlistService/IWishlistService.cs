﻿using AllBookedUp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Services.WishlistService
{
    public interface IWishlistService
    {
        Task AddToWishlist(Product product, string user);
        Task<List<CartItem>> GetWishlistItems(string user);
        Task DeleteItem(CartItem item);
    }
}
