using fourth_term_software_labs.Models;

namespace fourth_term_software_labs.Repositories
{
    public interface IBankAccountRepository
    {
        // Существующие методы
        IEnumerable<BankAccount> GetAll();
        BankAccount? GetById(int id);
        BankAccount? GetByAccountNumber(string accountNumber);
        void Add(BankAccount account);
        void Update(BankAccount account);
        void Delete(int id);
        IEnumerable<BankAccount> GetActiveAccounts();
        IEnumerable<BankAccount> GetByCurrency(string currency);

        // НОВЫЕ LINQ-МЕТОДЫ

        // 1. Фильтрация по балансу (диапазон цен -> диапазон баланса)
        IEnumerable<BankAccount> GetAccountsByBalanceRange(decimal minBalance, decimal maxBalance);

        // 2. Топ N самых богатых счетов (по балансу)
        IEnumerable<BankAccount> GetTopRichestAccounts(int count);

        // 3. Поиск по владельцу или номеру счета
        IEnumerable<BankAccount> SearchAccounts(string searchTerm);

        // 4. Статистика: средний баланс
        decimal GetAverageBalance();

        // 5. Общее количество счетов
        int GetTotalCount();

        // 6. Диапазон балансов (мин и макс)
        (decimal MinBalance, decimal MaxBalance) GetBalanceRange();

        // 7. Проверка наличия счетов в указанной валюте
        bool AnyInCurrency(string currency);

        // 8. Группировка по валюте
        IEnumerable<IGrouping<string, BankAccount>> GetAccountsGroupedByCurrency();

        // 9. Пагинация
        IEnumerable<BankAccount> GetAccountsWithPagination(int page, int pageSize);

        // 10. Общее количество страниц
        int GetTotalPages(int pageSize);

        // 11. АСИНХРОННЫЕ ВЕРСИИ
        Task<IEnumerable<BankAccount>> GetAllAsync();
        Task<BankAccount?> GetByIdAsync(int id);
        Task<IEnumerable<BankAccount>> GetAccountsByBalanceRangeAsync(decimal minBalance, decimal maxBalance);
        Task<decimal> GetAverageBalanceAsync();
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<IGrouping<string, BankAccount>>> GetAccountsGroupedByCurrencyAsync();
    }
}