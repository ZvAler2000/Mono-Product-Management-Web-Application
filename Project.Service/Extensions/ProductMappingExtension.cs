using Project.Service.DTOs;
using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Extensions
{
    public static class ProductMappingExtension
    {
        public static ProductDTO ToDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                CategoryName = product.Category?.Name
            };
        }

        public static Product ToEntity(this CreateProductDTO dto)
        {
            return new Product
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
