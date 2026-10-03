// Thư mục: DoanDEMVC/Controllers/SystemAccountController.cs
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DoanDEMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemAccountController : Controller
    {
        private readonly ISystemAccountService _accountService;

        public SystemAccountController(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAll();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult GetAccountForm(short? id)
        {
            if (id == null) return PartialView("_AccountForm", new SystemAccount());
            var acc = _accountService.GetById(id.Value);
            if (acc == null) return NotFound();
            return PartialView("_AccountForm", acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAccount(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                if (account.AccountID == 0) _accountService.Insert(account);
                else _accountService.Update(account);
                return Json(new { success = true });
            }
            return PartialView("_AccountForm", account);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            _accountService.Delete(id);
            TempData["SuccessMsg"] = "Account deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}