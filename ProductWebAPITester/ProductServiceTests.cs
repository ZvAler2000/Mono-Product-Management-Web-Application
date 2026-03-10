using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Service.Services;
using Project.Service.Entities;
using Project.Service.Interfaces;
using Project.Service.Common;
using Project.Service.DTOs;

namespace ProductWebAPITester
{
    [TestClass]
    public sealed class ProductServiceTests
    {
        [TestMethod]
        public async Task GetFilteredAsync_ShouldReturnCountOfAllItems()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService( context );

            //Act
            var result = await service.GetFilteredAsync(new ProductQuerryParameters());

            //Assert
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public async Task GetFilteredAsync_ShouldReturnCountOfAllItemsAsZeroFiltered()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.GetFilteredAsync(new ProductQuerryParameters { IsActive = false} );

            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetFilteredAsync_ShouldReturnCountOfFilteredItems()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.GetFilteredAsync(new ProductQuerryParameters { MinPrice = 250 });

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsTrueAfterFindingCorrectProductAndDeletingIt()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.DeleteAsync(2);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsFalseAfterNotFindingProductWithGivenId()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);
            
            //Act
            var result = await service.DeleteAsync(3);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsTrueAfterFindingAndUpdating()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.UpdateAsync(2, new UpdateProductDTO
            {
                CategoryId = 1,
                Name = "Samsung Galaxy S25",
                Price = 399,
                StockQuantity = 100,
                IsActive = true,
            });

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsFalseAfterNotFindingTheProduct()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.UpdateAsync(3, new UpdateProductDTO
            {
                CategoryId = 1,
                Name = "Samsung Galaxy S25",
                Price = 399,
                StockQuantity = 100,
                IsActive = true,
            });

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateAsync_ReturnsDTOsNameOfCreatedProduct()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.Add(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 12",
                    Price = 299,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                },

                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Samsung Galaxy S25",
                    Price = 199,
                    StockQuantity = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 3)
                }
            );

            await context.SaveChangesAsync();

            IProductService service = new ProductService(context);

            //Act
            var result = await service.CreateAsync( new CreateProductDTO
            {
                CategoryId = 1,
                Name = "Google Pixel 9a",
                Price = 499,
                StockQuantity = 100
            } );

            var dto = new CreateProductDTO
            {
                CategoryId = 1,
                Name = "Google Pixel 9a",
                Price = 499,
                StockQuantity = 100
            };

            //Assert
            Assert.AreEqual(dto.Name, result.Name);
        }
    }
}
