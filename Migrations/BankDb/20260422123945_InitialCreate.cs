using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fourth_term_software_labs.Migrations.BankDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, defaultValue: "RUB"),
                    OpenDate = table.Column<DateTime>(type: "date", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Currency", "InterestRate", "IsActive", "OpenDate", "OwnerName" },
                values: new object[,]
                {
                    { 1, "12345678901234567890", 150000.50m, "RUB", 5.5m, true, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иван Петров" },
                    { 2, "09876543210987654321", 5000.00m, "USD", 1.2m, true, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мария Сидорова" },
                    { 3, "11223344556677889900", 5000000.00m, "RUB", 8.0m, false, new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ООО 'Ромашка'" },
                    { 4, "99887766554433221100", 25000.00m, "EUR", 0.5m, true, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Анна Иванова" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountNumber",
                table: "BankAccounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_Currency",
                table: "BankAccounts",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_IsActive",
                table: "BankAccounts",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
