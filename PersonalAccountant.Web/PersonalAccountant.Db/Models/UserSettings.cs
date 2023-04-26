namespace PersonalAccountant.Db.Models;

public sealed record UserSettings(
	string Id,
	string Name,
	string Email,
	GasAccountSettings GasAccount,
	ElectricityAccountSettings ElectricityAccount,
	WaterAccountSettings WaterAccount);

public sealed record GasAccountSettings(string AccountNumber, string Login, string EncryptedPassword);

public sealed record ElectricityAccountSettings(string AccountNumber, string Login, string EncryptedPassword);

public sealed record WaterAccountSettings(string AccountNumber, string Login, string EncryptedPassword);
