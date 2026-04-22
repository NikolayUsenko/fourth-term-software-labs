using fourth_term_software_labs.Models;
using fourth_term_software_labs.Data;
using Microsoft.EntityFrameworkCore;

namespace fourth_term_software_labs.Repositories
{
    public class EFBankAccountRepository : IBankAccountRepository
    {
        private readonly BankDbContext _context; // Используем BankDbContext

        public EFBankAccountRepository(BankDbContext context) // Инжектим BankDbContext
        {
            _context = context;
        }

        public IEnumerable<BankAccount> GetAll()
        {
            return _context.BankAccounts
                .AsNoTracking()
                .ToList();
        }

        public BankAccount? GetById(int id)
        {
            return _context.BankAccounts
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id);
        }

        public BankAccount? GetByAccountNumber(string accountNumber)
        {
            return _context.BankAccounts
                .AsNoTracking()
                .FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void Add(BankAccount account)
        {
            _context.BankAccounts.Add(account);
            _context.SaveChanges();
        }

        public void Update(BankAccount account)
        {
            _context.BankAccounts.Update(account);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var account = _context.BankAccounts.Find(id);
            if (account != null)
            {
                _context.BankAccounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public IEnumerable<BankAccount> GetActiveAccounts()
        {
            return _context.BankAccounts
                .Where(a => a.IsActive)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<BankAccount> GetByCurrency(string currency)
        {
            return _context.BankAccounts
                .Where(a => a.Currency == currency)
                .AsNoTracking()
                .ToList();
        }
    }
}