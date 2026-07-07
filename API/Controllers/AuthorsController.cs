using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await authorService.GetByIdAsync(id);
            if (author is null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto input)
        {
            if (string.IsNullOrWhiteSpace(input.FirstName) && string.IsNullOrWhiteSpace(input.LastName))
                return BadRequest("Author name is required");

            var id = await authorService.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto input)
        {
            await authorService.UpdateAsync(id, input);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
