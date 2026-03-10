using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.DTOs;
using Project.Service.Extensions;
using Project.Service.Interfaces;

namespace Project.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductDBContext _context;

        public CategoryService(ProductDBContext context)
        {
            _context = context;
        }
        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var category = dto.ToEntity();

            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();

            return category.ToDTO();
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await _context.ProductCategories
                .Select(c => c.ToDTO())
                .ToListAsync();
        }
        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return null;
            }

            return category.ToDTO();
        }
        public async Task<bool> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _context.ProductCategories.FindAsync(id);

            if(category == null)
            {
                return false;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);

            if(category == null)
            {
                return false; 
            }

            if(await _context.Products.AnyAsync(p => p.CategoryId == id))
            {
                throw new ArgumentException("Cannot delete category that is still in use by any of the products.");
            }

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
