﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace PersonalAccountant.Web.Controllers;

public class CultureController : Controller
{
	[AllowAnonymous]
	[HttpPost]
	public IActionResult SetCulture(string culture, string returnUrl = "/")
	{
		Response.Cookies.Append(
			CookieRequestCultureProvider.DefaultCookieName,
			CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
			new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
		);

		return LocalRedirect(returnUrl);
	}
}