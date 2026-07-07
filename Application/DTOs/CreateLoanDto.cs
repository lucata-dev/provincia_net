using System;

namespace Application.DTOs
{
    public class CreateLoanDto
    {
        public int BookId { get; set; }

        public string BorrowerName { get; set; } = null!;

        public DateTime DueDate { get; set; }
    }
}
