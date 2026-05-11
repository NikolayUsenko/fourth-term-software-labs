namespace fourth_term_software_labs.Models
{
    /// <summary>
    /// ViewModel для страницы статистики банковских счетов
    /// </summary>
    public class BankAccountsStatisticsViewModel
    {
        /// <summary>
        /// Общее количество счетов
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Средний баланс всех счетов
        /// </summary>
        public decimal AverageBalance { get; set; }

        /// <summary>
        /// Количество активных счетов
        /// </summary>
        public int ActiveCount { get; set; }

        /// <summary>
        /// Диапазон балансов (минимальный и максимальный)
        /// </summary>
        public (decimal MinBalance, decimal MaxBalance) BalanceRange { get; set; }

        /// <summary>
        /// Список валют со статистикой по каждой
        /// </summary>
        public IEnumerable<CurrencyStatViewModel> Currencies { get; set; }
    }

    /// <summary>
    /// ViewModel для статистики по валюте
    /// </summary>
    public class CurrencyStatViewModel
    {
        /// <summary>
        /// Название валюты
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Количество счетов в валюте
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Общая сумма в валюте
        /// </summary>
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Средний баланс в валюте
        /// </summary>
        public decimal AverageBalance { get; set; }

        /// <summary>
        /// Минимальный баланс в валюте
        /// </summary>
        public decimal MinBalance { get; set; }

        /// <summary>
        /// Максимальный баланс в валюте
        /// </summary>
        public decimal MaxBalance { get; set; }
    }
}