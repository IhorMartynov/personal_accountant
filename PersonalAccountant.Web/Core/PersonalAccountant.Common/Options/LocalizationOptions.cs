namespace PersonalAccountant.Common.Options;

public sealed class LocalizationOptions
{
	public string DefaultCulture { get; set; } = "en-US";
	public CultureIcon[] AvailableCultures { get; set; } = Array.Empty<CultureIcon>();
}

public sealed class CultureIcon
{
	public string CultureCode { get; set; } = "";
	public string Image { get; set; } = "";
}