using System.Security.Claims;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AccountLoginModel model)
		{
			if (ModelState.IsValid)
			{
				UserModel userResult = new UserModel();
				var result = _accountService.Login(model, userResult);
				if (result.IsSuccessful)
				{
					List<Claim> claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, userResult.UserName),
						new Claim(ClaimTypes.Role, userResult.RoleName),
						new Claim(ClaimTypes.Sid, userResult.Id.ToString())
					};
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
					return RedirectToAction("Index", "Home", new { area = "" });

				}
				ModelState.AddModelError("", result.Message);
			}
			return View();
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home", new { area = "" });
		}
		public IActionResult AccessDenied()
		{
			return View("_Error", "You don't have access to this operation!");
		}
	}
}
