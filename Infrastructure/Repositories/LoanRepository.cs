using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext context;

        public LoanRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await this.context.Loans.AddAsync(loan);
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await this.context.Loans
                .Include(p => p.Book)
                .ToListAsync();
        }

        public Task<Loan?> GetLoanByIdAsync(int id)
        {
            return this.context.Loans
                .Include(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(Loan loan)
        {
            this.context.Loans.Remove(loan);
        }

        public Task SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Update(Loan loan)
        {
            this.context.Loans.Update(loan);
        }
    }
}