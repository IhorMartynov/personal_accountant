using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountant.Common.Options;
using PersonalAccountant.Web.Components.ViewModels;

namespace PersonalAccountant.Web.Components;

[ViewComponent]
public sealed class CurrentCultureViewComponent : ViewComponent
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly LocalizationOptions _localizationOptions;

	public CurrentCultureViewComponent(
		LocalizationOptions localizationOptions,
		IHttpContextAccessor httpContextAccessor)
	{
		this._localizationOptions = localizationOptions;
		this._httpContextAccessor = httpContextAccessor;
	}

	public Task<IViewComponentResult> InvokeAsync()
	{
		var requestCultureFeature = _httpContextAccessor.HttpContext!.Features.Get<IRequestCultureFeature>();

		var currentCulture = requestCultureFeature.RequestCulture.UICulture.Name;
		var cultureItems = _localizationOptions.AvailableCultures
			.Select(c => new CultureDetails(c.CultureCode, c.Image))
			.ToArray();
		var returnUrl = string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Request.Path)
			? "~/"
			: $"~{_httpContextAccessor.HttpContext.Request.Path}";

		var viewModel = new CurrentCultureViewModel(currentCulture, cultureItems, returnUrl);

		return Task.FromResult<IViewComponentResult>(View(viewModel));
	}
}