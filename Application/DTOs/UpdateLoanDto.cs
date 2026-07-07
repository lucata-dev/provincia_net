using System;

namespace Application.DTOs
{
    public class UpdateLoanDto
    {
        public DateTime? DueDate { get; set; }

        public DateTime? ReturnedDate { get; set; }
    }
}
