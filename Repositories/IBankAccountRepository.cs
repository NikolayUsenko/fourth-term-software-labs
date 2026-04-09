using fourth_term_software_labs.Models;

namespace fourth_term_software_labs.Repositories
{
    public interface IBankAccountRepository
    {
        IEnumerable<BankAccount> GetAll();
        BankAccount? GetById(int id);
        BankAccount? GetByAccountNumber(string accountNumber);
        void Add(BankAccount account);
        void Update(BankAccount account);
        void Delete(int id);
        IEnumerable<BankAccount> GetActiveAccounts();
        IEnumerable<BankAccount> GetByCurrency(string currency);
    }
}