using Microsoft.AspNetCore.Mvc;
using Project.Service.DTOs;
using Project.Service.Interfaces;
using Project.Service.Services;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var success = await _categoryService.UpdateAsync(id, dto);

            if(!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteAsync(id);

            if(!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
