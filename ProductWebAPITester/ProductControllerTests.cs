using Project.WebAPI.Controllers;
using Project.Service.Services;
using Project.Service.Interfaces;
using Project.Service.Extensions;
using Project.Service.Common;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Entities;
using Project.Service.DTOs;
using Microsoft.Identity.Client;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace ProductWebAPITester;

[TestClass]
public class ProductControllerTests
{
    [TestMethod]
    public async Task GetById_ReturnsOkObjectResultAndDTOOfFoundProductElement()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        var product = new Product
        {
            Id = 1,
            CategoryId = 1,
            Name = "iPhone 12",
            Price = 299,
            IsActive = true,
            StockQuantity = 100,
            CreatedAt = new DateTime(2026, 3, 3)
        };

        context.ProductCategories.Add(category);
        context.Products.Add(product);
        await context.SaveChangesAsync();

        //Act
        var result = await controller.GetById(1);

        //Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
        var actual = Xunit.Assert.IsType<ProductDTO>(okResult.Value);

        actual.Should().BeEquivalentTo(product.ToDTO());
    }

    [TestMethod]
    public async Task GetById_ReturnsException()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        var product = new Product
        {
            Id = 1,
            CategoryId = 1,
            Name = "iPhone 12",
            Price = 299,
            IsActive = true,
            StockQuantity = 100,
            CreatedAt = new DateTime(2026, 3, 3)
        };

        context.ProductCategories.Add(category);
        context.Products.Add(product);
        await context.SaveChangesAsync();

        Exception actual = null;

        //Act
        try
        {
            var result = await controller.GetById(2);
        }
        catch (Exception ex)
        {
            actual = ex;
        }

        //Assert
        //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Throws<NullReferenceException>(() => actual);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(actual);
    }

    [TestMethod]
    public async Task Create_Returns201Response()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        context.ProductCategories.Add(category);
        await context.SaveChangesAsync();

        var dto = new CreateProductDTO
        {
            CategoryId = 1,
            Name = "iPhone 12",
            Price = 199,
            StockQuantity = 10
        };
        
        //Act
        var result = await controller.Create(dto);

        //Assert
        var actual = Xunit.Assert.IsType<CreatedAtActionResult>(result);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(201, actual.StatusCode);
    }

    [TestMethod]
    public async Task GetFiltered_ReturnsOkObjectResultAndListOfProducts()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "iPhone 12",
                Price = 299,
                IsActive = true,
                StockQuantity = 100,
                CreatedAt = new DateTime(2026, 3, 3)
            },

            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "iPhone 13",
                Price = 499,
                IsActive = true,
                StockQuantity = 150,
                CreatedAt = new DateTime(2026, 3, 3)
            }
        };

        var expectedDto = new List<ProductDTO>
        {
            new ProductDTO
            {
                Id = 1,
                Name = "iPhone 12",
                Price = 299,
                IsActive = true,
                StockQuantity = 100,
                CreatedAt = new DateTime(2026, 3, 3),
                CategoryName = "Phone"
            },

            new ProductDTO
            {
                Id = 2,
                Name = "iPhone 13",
                Price = 499,
                IsActive = true,
                StockQuantity = 150,
                CreatedAt = new DateTime(2026, 3, 3),
                CategoryName = "Phone"
            }
        };

        var expected = new PagedResult<ProductDTO>(expectedDto, 2);

        context.ProductCategories.Add(category);
        context.Products.AddRange(products);
        await context.SaveChangesAsync();

        //Act
        var results = await controller.GetFiltered( new ProductQuerryParameters());

        //Assert
        var okObject = Xunit.Assert.IsType<OkObjectResult>(results);
        okObject.Value.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task Update_ReturnsNoContentAndStatus204()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "iPhone 12",
                Price = 299,
                IsActive = true,
                StockQuantity = 100,
                CreatedAt = new DateTime(2026, 3, 3)
            },

            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "iPhone 13",
                Price = 499,
                IsActive = true,
                StockQuantity = 150,
                CreatedAt = new DateTime(2026, 3, 3)
            }
        };

        context.ProductCategories.Add(category);
        context.Products.AddRange(products);
        await context.SaveChangesAsync();

        var dto = new UpdateProductDTO
        {
            CategoryId = 1,
            Name = "Samsung Galaxy S25",
            Price = 399,
            IsActive = true,
            StockQuantity = 100
        };

        //Act
        var result = await controller.Update(1, dto);

        //Assert
        var expected = Xunit.Assert.IsType<NoContentResult>(result);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(204, expected.StatusCode);
    }

    [TestMethod]
    public async Task Delete_Returns()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        IProductService service = new ProductService(context);
        ProductsController controller = new ProductsController(service);

        var category = new ProductCategory
        {
            Id = 1,
            Name = "Phone",
            Description = "Lorem ipsum"
        };

        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "iPhone 12",
                Price = 299,
                IsActive = true,
                StockQuantity = 100,
                CreatedAt = new DateTime(2026, 3, 3)
            },

            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "iPhone 13",
                Price = 499,
                IsActive = true,
                StockQuantity = 150,
                CreatedAt = new DateTime(2026, 3, 3)
            }
        };

        context.ProductCategories.Add(category);
        context.Products.AddRange(products);
        await context.SaveChangesAsync();


        //Act
        var result = await controller.Delete(2);

        //Assert
        var expected = Xunit.Assert.IsType<NoContentResult>(result);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(204, expected.StatusCode);
    }
}
