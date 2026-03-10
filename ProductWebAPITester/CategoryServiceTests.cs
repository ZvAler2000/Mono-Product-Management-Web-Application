using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Project.Service.DTOs;
using Project.Service.Entities;
using Project.Service.Interfaces;
using Project.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductWebAPITester
{
    [TestClass]
    public sealed class CategoryServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ReturnsAllCreatedCategories()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.AddRange(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                },

                new ProductCategory
                {
                    Id = 2,
                    Name = "Monitor",
                    Description = "Lorem ipsum."
                },

                new ProductCategory
                {
                    Id = 3,
                    Name = "Laptop",
                    Description = "Lorem ipsum."
                },

                new ProductCategory
                {
                    Id = 4,
                    Name = "GPU",
                    Description = "Lorem ipsum."
                },

                new ProductCategory
                {
                    Id = 5,
                    Name = "CPU",
                    Description = "Lorem ipsum."
                }
            );

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            List<CategoryDTO> expected = new List<CategoryDTO>
            {
                new CategoryDTO
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                },

                new CategoryDTO
                {
                    Id = 2,
                    Name = "Monitor",
                    Description = "Lorem ipsum."
                },

                new CategoryDTO
                {
                    Id = 3,
                    Name = "Laptop",
                    Description = "Lorem ipsum."
                },

                new CategoryDTO
                {
                    Id = 4,
                    Name = "GPU",
                    Description = "Lorem ipsum."
                },

                new CategoryDTO
                {
                    Id = 5,
                    Name = "CPU",
                    Description = "Lorem ipsum."
                }
            };

            //Act
            var actual = await service.GetAllAsync();

            //Assert
            actual.Should().BeEquivalentTo( expected );
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsDTOOfSelectedCategory()
        {
            //Arrange
            var context = ProductDBContextFactory.Create();

            context.ProductCategories.AddRange(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Lorem ipsum.",
                },

                new ProductCategory
                {
                    Id = 2,
                    Name = "Monitor",
                    Description = "Lorem ipsum."
                },

                new ProductCategory
                {
                    Id = 3,
                    Name = "Laptop",
                    Description = "Lorem ipsum."
                }
            );

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            CategoryDTO expected = new CategoryDTO
            {
                Id = 2,
                Name = "Monitor",
                Description = "Lorem ipsum."
            };

            //Act
            var actual = await service.GetByIdAsync(2);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnDTOOfCreatedCategory()
        {
            //Assume
            var context = ProductDBContextFactory.Create();

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            CategoryDTO expected = new CategoryDTO
            {
                Id = 1,
                Name = "Test",
                Description = "Lorem ipsum."
            };

            //Act
            var actual = await service.CreateAsync(new CreateCategoryDTO
            {
                Name = "Test",
                Description = "Lorem ipsum."
            });

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnTrueIfSuccessfullyUpdated()
        {
            //Assume
            var context = ProductDBContextFactory.Create();

            context.Add(new ProductCategory
            {
                Id = 1,
                Name = "Test",
                Description = "Lorem ipsum."
            });

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            //Act
            var actual = await service.UpdateAsync(1,  new UpdateCategoryDTO
            {
                Name = "Smartphone",
                Description = "Lorem ipsum"
            });

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnFalseIfIdNotFound()
        {
            //Assume
            var context = ProductDBContextFactory.Create();

            context.Add(new ProductCategory
            {
                Id = 1,
                Name = "Test",
                Description = "Lorem ipsum."
            });

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            //Act
            var actual = await service.UpdateAsync(2, new UpdateCategoryDTO
            {
                Name = "Smartphone",
                Description = "Lorem ipsum"
            });

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfIdFoundAndDeleted()
        {
            //Assume
            var context = ProductDBContextFactory.Create();

            context.Add(new ProductCategory
            {
                Id = 1,
                Name = "Test",
                Description = "Lorem ipsum."
            });

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            //Act
            var actual = await service.DeleteAsync(1);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfIdNotFound()
        {
            //Assume
            var context = ProductDBContextFactory.Create();

            context.Add(new ProductCategory
            {
                Id = 1,
                Name = "Test",
                Description = "Lorem ipsum."
            });

            await context.SaveChangesAsync();

            ICategoryService service = new CategoryService(context);

            //Act
            var actual = await service.DeleteAsync(2);

            //Assert
            Assert.IsFalse(actual);
        }
    }
}
