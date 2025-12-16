using Microsoft.EntityFrameworkCore;
using POS.Backend.Data;
using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public class CashClosingRepository(AppDbContext context) : ICashClosingRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(CashClosing closing)
        {
            _context.CashClosings.Add(closing);
            await _context.SaveChangesAsync();
        }

        public async Task<CashClosing?> GetLastClosingAsync()
        {
            return await _context.CashClosings
                .OrderByDescending(c => c.EndDate)
                .FirstOrDefaultAsync();
        }
    }
}
