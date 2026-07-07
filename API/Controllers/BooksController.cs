using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _svc;

        public BooksController(IBookService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _svc.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _svc.GetByIdAsync(id);
            if (book is null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Title)) return BadRequest("Title is required");

            var id = await _svc.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto input)
        {
            await _svc.UpdateAsync(id, input);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id:int}/loan")]
        public async Task<IActionResult> Loan(int id, [FromBody] LoanRequest request)
        {
            await _svc.LoanAsync(id, request.BorrowerName, request.DueDate);
            return Ok();
        }

        [HttpPost("return/{prestamoId:int}")]
        public async Task<IActionResult> Return(int prestamoId)
        {
            await _svc.ReturnAsync(prestamoId);
            return Ok();
        }

        public class LoanRequest
        {
            public string BorrowerName { get; set; } = null!;
            public DateTime DueDate { get; set; }
        }
    }
}
