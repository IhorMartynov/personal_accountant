using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonalAccountant.Web.Controllers
{
	public class AccountController : Controller
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login(string? returnUrl)
		{
			return Challenge(GoogleDefaults.AuthenticationScheme);
		}
	}
}
