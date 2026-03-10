using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Service.Entities;

namespace Project.Service.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Handheld electronic device used for communication."
                },

                new ProductCategory
                {
                    Id = 2,
                    Name = "Monitor",
                    Description = "Output device that displays visual information sent from the computer."
                },

                new ProductCategory
                {
                    Id = 3,
                    Name = "Sneakers",
                    Description = "Street/sports footwear."
                },

                new ProductCategory
                {
                    Id = 4,
                    Name = "Shirt",
                    Description = "Cloth garment for upper body worn by both men and women."
                },

                new ProductCategory
                {
                    Id = 5,
                    Name = "Perfume",
                    Description = "Mixture of fragrant oils and aromas that give a pleasant smell."
                },

                new ProductCategory
                {
                    Id = 6,
                    Name = "Book",
                    Description = "Written work created by one or more authors."
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 899,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3),
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 2,
                    Name = "Samsung Odyssey OLED G9",
                    Price = 1300,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3),
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 799,
                    StockQuantity = 150,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3),
                },

                new Product
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "HP E24 G4",
                    Price = 99,
                    StockQuantity = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 5,
                    CategoryId = 3,
                    Name = "Air Jordan 1s",
                    Price = 299,
                    StockQuantity = 50,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 6,
                    CategoryId = 3,
                    Name = "Nike Air Force 1s",
                    Price = 120,
                    StockQuantity = 200,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 7,
                    CategoryId = 4,
                    Name = "Comme de Garcons Graphic-print sweatshirt",
                    Price = 77,
                    StockQuantity = 20,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 8,
                    CategoryId = 4,
                    Name = "Zara relaxed fit flowing brown shirt",
                    Price = 35,
                    StockQuantity = 250,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 9,
                    CategoryId = 5,
                    Name = "Calvin Klein Obsession 30ml",
                    Price = 20,
                    StockQuantity = 90,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 10,
                    CategoryId = 5,
                    Name = "Chanel no. 5 eau de parfum 100ml",
                    Price = 185,
                    StockQuantity = 70,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 11,
                    CategoryId = 6,
                    Name = "Blood Meridian by Cormack McCarthy",
                    Price = 15,
                    StockQuantity = 500,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 12,
                    CategoryId = 6,
                    Name = "Darth Plagueis by James Luceno",
                    Price = 20,
                    StockQuantity = 450,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
