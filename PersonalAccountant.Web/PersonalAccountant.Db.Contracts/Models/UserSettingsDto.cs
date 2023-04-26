namespace PersonalAccountant.Db.Contracts.Models;

public sealed record UserSettingsDto(
	string Name,
	string Email,
	GasAccountSettingsDto GasAccount,
	ElectricityAccountSettingsDto ElectricityAccount,
	WaterAccountSettingsDto WaterAccount);

public sealed record GasAccountSettingsDto(string AccountNumber, string Login, string Password);

public sealed record ElectricityAccountSettingsDto(string AccountNumber, string Login, string Password);

public sealed record WaterAccountSettingsDto(string AccountNumber, string Login, string Password);
