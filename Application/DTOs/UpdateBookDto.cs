using System;

namespace Application.DTOs
{
    public class UpdateBookDto
    {
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? CopiesAvailable { get; set; }
    }
}
