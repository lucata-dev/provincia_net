using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cats = await this.categoryService.GetAllAsync();
            return Ok(cats);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await this.categoryService.GetByIdAsync(id);
            if (cat is null) return NotFound();
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name)) return BadRequest("Name required");

            var id = await this.categoryService.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto input)
        {
            await this.categoryService.UpdateAsync(id, input);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
