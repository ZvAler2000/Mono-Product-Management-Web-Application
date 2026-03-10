using Project.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto);
        Task<bool> UpdateAsync(int id, UpdateCategoryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
