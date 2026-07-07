using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string? ISBN { get; set; }

        public string? Synopsis { get; set; }

        public int PublicationYear { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
