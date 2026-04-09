using fourth_term_software_labs.Models;
using fourth_term_software_labs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace fourth_term_software_labs.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountsController(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        // GET: /BankAccounts
        public IActionResult Index()
        {
            var accounts = _repository.GetAll();
            return View(accounts);
        }

        // GET: /BankAccounts/Details/5
        public IActionResult Details(int id)
        {
            var account = _repository.GetById(id);
            if (account == null)
                return NotFound();
            return View(account);
        }

        // GET: /BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BankAccount account)
        {
            // Дополнительная проверка: номер счета должен быть уникальным
            if (_repository.GetByAccountNumber(account.AccountNumber) != null)
            {
                ModelState.AddModelError(nameof(account.AccountNumber), "Счет с таким номером уже существует.");
            }

            if (ModelState.IsValid)
            {
                _repository.Add(account);
                TempData["SuccessMessage"] = "Банковский счет успешно открыт!";
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: /BankAccounts/Edit/5
        public IActionResult Edit(int id)
        {
            var account = _repository.GetById(id);
            if (account == null)
                return NotFound();
            return View(account);
        }

        // POST: /BankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BankAccount account)
        {
            if (id != account.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(account);
                    TempData["SuccessMessage"] = "Данные счета обновлены!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(account);
        }

        // GET: /BankAccounts/Delete/5
        public IActionResult Delete(int id)
        {
            var account = _repository.GetById(id);
            if (account == null)
                return NotFound();
            return View(account);
        }

        // POST: /BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Счет удален!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /BankAccounts/Active
        public IActionResult Active()
        {
            var accounts = _repository.GetActiveAccounts();
            ViewBag.Filter = "Активные счета";
            return View("Index", accounts);
        }

        // GET: /BankAccounts/Currency/RUB
        public IActionResult Currency(string currency)
        {
            var accounts = _repository.GetByCurrency(currency);
            ViewBag.Filter = $"Счета в валюте {currency}";
            return View("Index", accounts);
        }
    }
}