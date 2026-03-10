using Project.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Interfaces;
using Project.Service.Services;
using Project.WebAPI.Controllers;
using Project.Service.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProductWebAPITester;

[TestClass]
public class CategoryControllerTests
{
    [TestMethod]
    public async Task GetAll_ReturnsOkStatusAndListOfCategories()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        context.AddRange(
            new ProductCategory
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            },
            new ProductCategory
            {
                Id = 2,
                Name = "Laptop",
                Description = "Lorem ipsum."
            },
            new ProductCategory
            {
                Id = 3,
                Name = "Fridge",
                Description = "Lorem ipsum."
            }
        );

        await context.SaveChangesAsync();

        var expected = new List<CategoryDTO>
        {
            new CategoryDTO
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            },

            new CategoryDTO
            {
                Id = 2,
                Name = "Laptop",
                Description = "Lorem ipsum."
            },

            new CategoryDTO
            {
                Id = 3,
                Name = "Fridge",
                Description = "Lorem ipsum."
            }
        };

        //Act
        var actual = await controller.GetAll();

        //Assert
        var okStatus = Xunit.Assert.IsType<OkObjectResult>(actual);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(200, okStatus.StatusCode);
        okStatus.Value.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task GetById_ReturnsOkStatusAndDtoOfFoundCategory()
    {
        //Assume
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        context.AddRange(
            new ProductCategory
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            },
            new ProductCategory
            {
                Id = 2,
                Name = "Laptop",
                Description = "Lorem ipsum."
            },
            new ProductCategory
            {
                Id = 3,
                Name = "Fridge",
                Description = "Lorem ipsum."
            }
        );

        await context.SaveChangesAsync();

        var expected = new CategoryDTO
        {
            Id = 2,
            Name = "Laptop",
            Description = "Lorem ipsum."
        };

        //Act
        var result = await controller.GetById(2);

        //Assert
        var okStatus = Xunit.Assert.IsType<OkObjectResult>(result);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(okStatus.StatusCode, 200);
        okStatus.Value.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task Create_ReturnsCreatedAtActionAndDTOOfCreatedCategory()
    {
        //Assume
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        var dto = new CreateCategoryDTO
        {
            Name = "Laptop",
            Description = "Lorem ipsum."
        };

        var expected = new CategoryDTO
        {
            Id = 1,
            Name = "Laptop",
            Description = "Lorem ipsum."
        };

        //Act
        var result = await controller.Create(dto);

        //Assert
        var status = Xunit.Assert.IsType<CreatedAtActionResult>(result);
        status.Value.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task Update_ReturnsNoContentResult()
    {
        //Assume
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        context.Add(
            new ProductCategory
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            }
        );

        await context.SaveChangesAsync();

        var dto = new UpdateCategoryDTO
        {
            Name = "Laptop",
            Description = "Lorem ipsum."
        };

        //Act
        var result = await controller.Update(1, dto);

        //Assert
        Xunit.Assert.IsType<NoContentResult>(result);
    }

    [TestMethod]
    public async Task Delete_ReturnsNoContentResultAfterFindingIdAndDeletingElement()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        context.Add(
            new ProductCategory
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            }
        );

        //Act
        var status = await controller.Delete(1);

        //Assert
        Xunit.Assert.IsType<NoContentResult>(status);
    }

    [TestMethod]
    public async Task Delete_ReturnsNotFoundResultWhenElementWithIdDoesNotExist()
    {
        //Arrange
        var context = ProductDBContextFactory.Create();
        ICategoryService service = new CategoryService(context);
        CategoryController controller = new CategoryController(service);

        context.Add(
            new ProductCategory
            {
                Id = 1,
                Name = "Phone",
                Description = "Lorem ipsum."
            }
        );

        //Act
        var status = await controller.Delete(2);

        //Assert
        Xunit.Assert.IsType<NotFoundResult>(status);
    }
}
