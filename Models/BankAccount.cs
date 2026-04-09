using System.ComponentModel.DataAnnotations;

namespace fourth_term_software_labs.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            AccountNumber = string.Empty;
            OwnerName = string.Empty;
            Currency = "RUB";
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Номер счета обязателен")]
        [StringLength(20, MinimumLength = 20, ErrorMessage = "Номер счета должен содержать ровно 20 цифр")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "Номер счета должен состоять ровно из 20 цифр")]
        [Display(Name = "Номер счета")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Имя владельца обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 100 символов")]
        [Display(Name = "Владелец счета")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Баланс обязателен")]
        [Range(0, 10000000, ErrorMessage = "Баланс должен быть от 0 до 10 000 000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Валюта обязательна")]
        [RegularExpression("^(RUB|USD|EUR)$", ErrorMessage = "Валюта должна быть RUB, USD или EUR")]
        [Display(Name = "Валюта")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Дата открытия обязательна")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата открытия")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(BankAccount), nameof(ValidateOpenDate))]
        public DateTime OpenDate { get; set; }

        [Required(ErrorMessage = "Процентная ставка обязательна")]
        [Range(0, 20, ErrorMessage = "Ставка должна быть от 0 до 20%")]
        [Display(Name = "Процентная ставка (%)")]
        public decimal InterestRate { get; set; }

        [Display(Name = "Счет активен")]
        public bool IsActive { get; set; }

        // Кастомная валидация даты (не в будущем)
        public static ValidationResult? ValidateOpenDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Дата открытия не может быть в будущем.");
            }
            return ValidationResult.Success;
        }

        // Метод для расчета процентов
        [Display(Name = "Сумма процентов")]
        public decimal CalculateInterest()
        {
            if (!IsActive)
                return 0;

            // Простой расчет процентов: Баланс * (Ставка/100)
            return Balance * (InterestRate / 100);
        }

        // Метод для получения возраста счета в годах
        public int GetAccountAgeInYears()
        {
            var today = DateTime.Today;
            var age = today.Year - OpenDate.Year;
            if (OpenDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}