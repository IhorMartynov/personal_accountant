using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountant.Web.Models;

namespace PersonalAccountant.Web.Controllers;

public class HomeController : Controller
{
	[AllowAnonymous]
	public IActionResult Index()
	{
		return View();
	}

	[Authorize]
	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}