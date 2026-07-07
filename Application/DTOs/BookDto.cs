using System;

namespace Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int CopiesAvailable { get; set; }

        public string? AuthorName { get; set; }
        public string? CategoryName { get; set; }
    }
}
