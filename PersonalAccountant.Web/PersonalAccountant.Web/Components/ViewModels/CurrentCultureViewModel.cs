namespace PersonalAccountant.Web.Components.ViewModels;

public sealed record CurrentCultureViewModel(string CurrentCulture, CultureDetails[] AvailableCultures, string ReturnUrl);

public sealed record CultureDetails(string CultureCode, string ImagePath);