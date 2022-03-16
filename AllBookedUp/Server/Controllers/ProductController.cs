using AllBookedUp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllBookedUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product> {
            new Product
            {
            Id = 1,
            Title = "The Hitchhiker's Guide to the Galaxy",
            Description = "Test book for the hitch hikers",
            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
            Price = 9.99m
        },
        new Product
        {
            Id = 2,
            Title = "Test book 2 ",
            Description = "Test book for ID 2",
            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/cf/SoLongAndThanksForAllTheFish.jpg",
            Price = 7.99m
        },
        new Product
        {
            Id = 3,
            Title = "Test book 3",
            Description = "Test book for the hitch hikers",
            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d3/Life%2C_The_Universe_and_Everything_cover.jpg",
            Price = 6.99m
        }
    };

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(Products);
        }

    }
}
