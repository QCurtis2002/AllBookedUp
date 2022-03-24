using AllBookedUp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
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
                    Description = "Test book for the ID 3",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d3/Life%2C_The_Universe_and_Everything_cover.jpg",
                    Price = 6.99m
                }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
