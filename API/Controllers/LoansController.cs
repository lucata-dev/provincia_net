using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService loanService;

        public LoansController(ILoanService loanService)
        {
            this.loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await loanService.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var loan = await loanService.GetByIdAsync(id);
            if (loan is null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoanDto input)
        {
            if (input.BookId <= 0 || string.IsNullOrWhiteSpace(input.BorrowerName))
                return BadRequest("BookId and BorrowerName are required");

            var id = await loanService.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLoanDto input)
        {
            await loanService.UpdateAsync(id, input);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await loanService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("return/{loanId:int}")]
        public async Task<IActionResult> Return(int loanId)
        {
            await loanService.ReturnAsync(loanId);
            return Ok();
        }
    }
}
