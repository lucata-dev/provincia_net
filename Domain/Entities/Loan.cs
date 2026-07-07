using System;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }

        public string BorrowerName { get; set; } = null!;

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public bool IsReturned => ReturnedDate.HasValue;
    }
}
