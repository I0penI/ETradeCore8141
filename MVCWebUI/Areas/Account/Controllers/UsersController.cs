using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace MVCWebUI.Areas.Account.Controllers
{
    [Area("Account")]
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.Register(model);
                if (result.IsSuccessful)
                    return RedirectToAction("Login");
                ModelState.AddModelError("", result.Message);
            }
            return View();
        }
    }
}
