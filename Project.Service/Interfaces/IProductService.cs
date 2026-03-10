using System;
using System.Collections.Generic;
using System.Text;
using Project.Service.Common;
using Project.Service.DTOs;

namespace Project.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task<bool> UpdateAsync(int id, UpdateProductDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<ProductDTO>> GetFilteredAsync(ProductQuerryParameters query);
    }
}
