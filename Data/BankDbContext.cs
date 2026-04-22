using fourth_term_software_labs.Models;
using Microsoft.EntityFrameworkCore;

namespace fourth_term_software_labs.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка таблицы BankAccount
            modelBuilder.Entity<BankAccount>(entity =>
            {
                // Первичный ключ
                entity.HasKey(e => e.Id);

                // Настройка AccountNumber
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(false);

                // Уникальный индекс для AccountNumber
                entity.HasIndex(e => e.AccountNumber)
                    .IsUnique();

                // Настройка OwnerName
                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(100);

                // Настройка Balance
                entity.Property(e => e.Balance)
                    .HasPrecision(18, 2) // 18 цифр всего, 2 после запятой
                    .IsRequired();

                // Настройка Currency
                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValue("RUB");

                // Настройка OpenDate
                entity.Property(e => e.OpenDate)
                    .IsRequired()
                    .HasColumnType("date");

                // Настройка InterestRate
                entity.Property(e => e.InterestRate)
                    .HasPrecision(5, 2)
                    .IsRequired();

                // Индекс для IsActive (для частых запросов активных счетов)
                entity.HasIndex(e => e.IsActive);

                // Индекс для Currency (для фильтрации по валюте)
                entity.HasIndex(e => e.Currency);
            });

            // Добавление начальных тестовых данных (как в InMemory репозитории)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Те же тестовые данные, что были в InMemory репозитории
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount
                {
                    Id = 1,
                    AccountNumber = "12345678901234567890",
                    OwnerName = "Иван Петров",
                    Balance = 150000.50m,
                    Currency = "RUB",
                    OpenDate = new DateTime(2020, 5, 15),
                    InterestRate = 5.5m,
                    IsActive = true
                },
                new BankAccount
                {
                    Id = 2,
                    AccountNumber = "09876543210987654321",
                    OwnerName = "Мария Сидорова",
                    Balance = 5000.00m,
                    Currency = "USD",
                    OpenDate = new DateTime(2022, 3, 10),
                    InterestRate = 1.2m,
                    IsActive = true
                },
                new BankAccount
                {
                    Id = 3,
                    AccountNumber = "11223344556677889900",
                    OwnerName = "ООО 'Ромашка'",
                    Balance = 5000000.00m,
                    Currency = "RUB",
                    OpenDate = new DateTime(2018, 11, 1),
                    InterestRate = 8.0m,
                    IsActive = false
                },
                new BankAccount
                {
                    Id = 4,
                    AccountNumber = "99887766554433221100",
                    OwnerName = "Анна Иванова",
                    Balance = 25000.00m,
                    Currency = "EUR",
                    OpenDate = new DateTime(2023, 1, 20),
                    InterestRate = 0.5m,
                    IsActive = true
                }
            );
        }
    }
}