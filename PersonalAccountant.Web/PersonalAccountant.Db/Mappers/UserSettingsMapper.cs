using Microsoft.AspNetCore.DataProtection;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

internal sealed class UserSettingsMapper : IUserSettingsMapper
{
	private readonly IDataProtector _dataProtector;

	public UserSettingsMapper(IDataProtectionProvider protectionProvider)
	{
		_dataProtector = protectionProvider.CreateProtector(nameof(UserSettings));
	}


	/// <inheritdoc/>
	public UserSettings Map(UserSettingsDto userSettings) =>
		Map(Guid.NewGuid().ToString(), userSettings);

	/// <inheritdoc/>
	public UserSettings Map(string? id, UserSettingsDto userSettings)
		=> new(
			id,
			userSettings.Name,
			userSettings.Email,
			new GasAccountSettings(
				userSettings.GasAccount.PublicEnterpriseId,
				userSettings.GasAccount.AccountNumber,
				userSettings.GasAccount.Login,
				_dataProtector.Protect(userSettings.GasAccount.Password)),
			new ElectricityAccountSettings(
                userSettings.ElectricityAccount.PublicEnterpriseId,
				userSettings.ElectricityAccount.AccountNumber,
				userSettings.ElectricityAccount.Login,
				_dataProtector.Protect(userSettings.ElectricityAccount.Password)),
			new WaterAccountSettings(
                userSettings.WaterAccount.PublicEnterpriseId,
				userSettings.WaterAccount.AccountNumber,
				userSettings.WaterAccount.Login,
				_dataProtector.Protect(userSettings.WaterAccount.Password)));
}