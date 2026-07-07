using System;

namespace Application.DTOs
{
    public class UpdateAuthorDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }
    }
}
