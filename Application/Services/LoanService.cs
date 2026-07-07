using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        public readonly ILoanRepository loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }
        public async Task<int> CreateAsync(CreateLoanDto dto)
        {
            var loan = new Domain.Entities.Loan
            {
                BookId = dto.BookId,
                BorrowerName = dto.BorrowerName,
                LoanDate = DateTime.UtcNow,
                DueDate = dto.DueDate
            };

            await loanRepository.AddAsync(loan);
            await loanRepository.SaveChangesAsync();
            return loan.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await loanRepository.GetLoanByIdAsync(id);
            if (loan is null) return;
            loanRepository.Remove(loan);
            await loanRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanDto>> GetAllAsync()
        {
            var loans = await loanRepository.GetAllAsync();
            return loans.Select(l => new LoanDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BorrowerName = l.BorrowerName,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                BookTitle = l.Book?.Title
            }).ToList();
        }

        public async Task<LoanDto?> GetByIdAsync(int id)
        {
            var l = await loanRepository.GetLoanByIdAsync(id);
            if (l is null) return null;
            return new LoanDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BorrowerName = l.BorrowerName,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                BookTitle = l.Book?.Title
            };
        }

        public async Task ReturnAsync(int loanId)
        {
            var loan = await loanRepository.GetLoanByIdAsync(loanId);
            if (loan is null) throw new InvalidOperationException("Préstamo no encontrado");
            if (loan.IsReturned) return;

            loan.ReturnedDate = DateTime.UtcNow;
            // Si el Book se incluye, aumentar copias disponibles
            if (loan.Book is not null)
            {
                loan.Book.CopiesAvailable += 1;
            }

            loanRepository.Update(loan);
            await loanRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateLoanDto dto)
        {
            var loan = await loanRepository.GetLoanByIdAsync(id);
            if (loan is null) return;

            if (dto.DueDate.HasValue) loan.DueDate = dto.DueDate.Value;
            if (dto.ReturnedDate.HasValue) loan.ReturnedDate = dto.ReturnedDate.Value;

            loanRepository.Update(loan);
            await loanRepository.SaveChangesAsync();
        }
    }
}
