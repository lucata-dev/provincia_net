using System;

namespace Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BorrowerName { get; set; } = null!;

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public string? BookTitle { get; set; }
    }
}
