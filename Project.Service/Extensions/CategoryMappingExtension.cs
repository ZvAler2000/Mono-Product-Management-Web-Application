using System;
using System.Collections.Generic;
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
    }
}
