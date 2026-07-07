using System;

namespace Application.DTOs
{
    public class CreateAuthorDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }
    }
}
