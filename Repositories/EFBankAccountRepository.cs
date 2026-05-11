using fourth_term_software_labs.Models;
using fourth_term_software_labs.Data;
using Microsoft.EntityFrameworkCore;

namespace fourth_term_software_labs.Repositories
{
    public class EFBankAccountRepository : IBankAccountRepository
    {
        private readonly BankDbContext _context;

        public EFBankAccountRepository(BankDbContext context)
        {
            _context = context;
        }

        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========
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

        // ========== НОВЫЕ LINQ-МЕТОДЫ ==========

        /// <summary>
        /// Фильтрация счетов по диапазону баланса
        /// </summary>
        public IEnumerable<BankAccount> GetAccountsByBalanceRange(decimal minBalance, decimal maxBalance)
        {
            return _context.BankAccounts
                .Where(a => a.Balance >= minBalance && a.Balance <= maxBalance)
                .OrderBy(a => a.Balance)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Топ N самых богатых счетов
        /// </summary>
        public IEnumerable<BankAccount> GetTopRichestAccounts(int count)
        {
            return _context.BankAccounts
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.Balance)
                .Take(count)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Поиск счетов по владельцу или номеру счета
        /// </summary>
        public IEnumerable<BankAccount> SearchAccounts(string searchTerm)
        {
            return _context.BankAccounts
                .Where(a => a.OwnerName.Contains(searchTerm) ||
                            a.AccountNumber.Contains(searchTerm))
                .OrderBy(a => a.OwnerName)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Средний баланс всех счетов
        /// </summary>
        public decimal GetAverageBalance()
        {
            return _context.BankAccounts.Average(a => a.Balance);
        }

        /// <summary>
        /// Общее количество счетов
        /// </summary>
        public int GetTotalCount()
        {
            return _context.BankAccounts.Count();
        }

        /// <summary>
        /// Диапазон балансов (минимальный и максимальный)
        /// </summary>
        public (decimal MinBalance, decimal MaxBalance) GetBalanceRange()
        {
            return (
                MinBalance: _context.BankAccounts.Min(a => a.Balance),
                MaxBalance: _context.BankAccounts.Max(a => a.Balance)
            );
        }

        /// <summary>
        /// Проверка наличия счетов в указанной валюте
        /// </summary>
        public bool AnyInCurrency(string currency)
        {
            return _context.BankAccounts.Any(a => a.Currency == currency);
        }

        /// <summary>
        /// Группировка счетов по валюте
        /// </summary>
        public IEnumerable<IGrouping<string, BankAccount>> GetAccountsGroupedByCurrency()
        {
            // Сначала получаем данные из БД
            var accounts = _context.BankAccounts
                .AsNoTracking()
                .ToList();

            // Затем выполняем группировку и сортировку в памяти
            return accounts
                .GroupBy(a => a.Currency)
                .OrderBy(g => g.Key)
                .ToList();
        }

        /// <summary>
        /// Пагинация: получение счетов для указанной страницы
        /// </summary>
        public IEnumerable<BankAccount> GetAccountsWithPagination(int page, int pageSize)
        {
            return _context.BankAccounts
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int GetTotalPages(int pageSize)
        {
            int totalCount = _context.BankAccounts.Count();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        // ========== АСИНХРОННЫЕ ВЕРСИИ ==========

        public async Task<IEnumerable<BankAccount>> GetAllAsync()
        {
            return await _context.BankAccounts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BankAccount?> GetByIdAsync(int id)
        {
            return await _context.BankAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<BankAccount>> GetAccountsByBalanceRangeAsync(decimal minBalance, decimal maxBalance)
        {
            return await _context.BankAccounts
                .Where(a => a.Balance >= minBalance && a.Balance <= maxBalance)
                .OrderBy(a => a.Balance)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> GetAverageBalanceAsync()
        {
            return await _context.BankAccounts.AverageAsync(a => a.Balance);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.BankAccounts.CountAsync();
        }

        public async Task<IEnumerable<IGrouping<string, BankAccount>>> GetAccountsGroupedByCurrencyAsync()
        {
            var accounts = await _context.BankAccounts
                .AsNoTracking()
                .ToListAsync();

            return accounts
                .GroupBy(a => a.Currency)
                .OrderBy(g => g.Key)
                .ToList();
        }
    }
}