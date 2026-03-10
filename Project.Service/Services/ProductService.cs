using System;
using System.Collections.Generic;
using System.Text;
using Project.Service.Interfaces;
using Project.Service.Data;
using Project.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using Project.Service.Extensions;
using System.Diagnostics.CodeAnalysis;
using Project.Service.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDBContext _context;

        public ProductService(ProductDBContext context)
        {
            _context = context;
        }
        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = dto.ToEntity();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            await _context.Entry(product)
                .Reference(p => p.Category)
                .LoadAsync();

            return product.ToDTO();
        }
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => p.ToDTO())
                .ToListAsync();
        }
        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product.ToDTO();
        }
        public async Task<bool> UpdateAsync(int id, UpdateProductDTO dto)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.StockQuantity = dto.StockQuantity;
            product.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<PagedResult<ProductDTO>> GetFilteredAsync(ProductQuerryParameters query)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if(query.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId ==  query.CategoryId);
            }
            if(query.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >=  query.MinPrice);
            }
            if(query.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= query.MaxPrice);
            }
            if(query.IsActive.HasValue)
            {
                products = products.Where(p => p.IsActive == query.IsActive);
            }

            products = query.SortBy?.ToLower() switch
            {
                "price" => products.OrderBy(p => p.Price),
                "name" => products.OrderBy(p => p.Name),
                "createdat" => products.OrderBy(p => p.CreatedAt),
                _ => products.OrderBy(p => p.Id)
            };

            var count = await products.CountAsync();

            if(query.Page < 1)
            {
                query.Page = 1;
            }
            if(query.PageSize < 1)
            {
                query.PageSize = 10;
            }
            if (query.PageSize > 100)
            {
                query.PageSize = 100;
            }

            var items = await products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(p => p.ToDTO())
                .ToListAsync();

            return new PagedResult<ProductDTO>(items, count);
        }
    }
}
