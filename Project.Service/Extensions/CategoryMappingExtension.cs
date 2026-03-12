using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Project.Service.DTOs;
using Project.Service.Entities;

namespace Project.Service.Extensions
{
    public static class CategoryMappingExtension
    {
        public static CategoryDTO ToDTO(this ProductCategory category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static ProductCategory ToEntity(this CreateCategoryDTO dto)
        {
            return new ProductCategory
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }

        public static Expression<Func<ProductCategory, CategoryDTO>> ToDTOExpression =
            category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
    }
}
