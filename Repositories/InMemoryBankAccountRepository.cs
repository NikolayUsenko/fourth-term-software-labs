using fourth_term_software_labs.Models;
using fourth_term_software_labs.Repositories;

namespace fourth_term_software_labs.Repositories
{
    public class InMemoryBankAccountRepository : IBankAccountRepository
    {
        private readonly List<BankAccount> _accounts;
        private int _nextId = 1;

        public InMemoryBankAccountRepository()
        {
            _accounts = new List<BankAccount>();
            SeedData();
        }

        private void SeedData()
        {
            // Тестовые данные (минимум 3 записи)
            Add(new BankAccount
            {
                AccountNumber = "12345678901234567890",
                OwnerName = "Иван Петров",
                Balance = 150000.50m,
                Currency = "RUB",
                OpenDate = new DateTime(2020, 5, 15),
                InterestRate = 5.5m,
                IsActive = true
            });

            Add(new BankAccount
            {
                AccountNumber = "09876543210987654321",
                OwnerName = "Мария Сидорова",
                Balance = 5000.00m,
                Currency = "USD",
                OpenDate = new DateTime(2022, 3, 10),
                InterestRate = 1.2m,
                IsActive = true
            });

            Add(new BankAccount
            {
                AccountNumber = "11223344556677889900",
                OwnerName = "ООО 'Ромашка'",
                Balance = 5000000.00m,
                Currency = "RUB",
                OpenDate = new DateTime(2018, 11, 1),
                InterestRate = 8.0m,
                IsActive = false
            });

            Add(new BankAccount
            {
                AccountNumber = "99887766554433221100",
                OwnerName = "Анна Иванова",
                Balance = 25000.00m,
                Currency = "EUR",
                OpenDate = new DateTime(2023, 1, 20),
                InterestRate = 0.5m,
                IsActive = true
            });
        }

        public IEnumerable<BankAccount> GetAll() => _accounts;

        public BankAccount? GetById(int id) => _accounts.FirstOrDefault(a => a.Id == id);

        public BankAccount? GetByAccountNumber(string accountNumber) =>
            _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        public void Add(BankAccount account)
        {
            account.Id = _nextId++;
            _accounts.Add(account);
        }

        public void Update(BankAccount account)
        {
            var existing = GetById(account.Id);
            if (existing != null)
            {
                existing.AccountNumber = account.AccountNumber;
                existing.OwnerName = account.OwnerName;
                existing.Balance = account.Balance;
                existing.Currency = account.Currency;
                existing.OpenDate = account.OpenDate;
                existing.InterestRate = account.InterestRate;
                existing.IsActive = account.IsActive;
            }
        }

        public void Delete(int id)
        {
            var account = GetById(id);
            if (account != null)
                _accounts.Remove(account);
        }

        public IEnumerable<BankAccount> GetActiveAccounts() =>
            _accounts.Where(a => a.IsActive);

        public IEnumerable<BankAccount> GetByCurrency(string currency) =>
            _accounts.Where(a => a.Currency.Equals(currency, StringComparison.OrdinalIgnoreCase));
    }
}