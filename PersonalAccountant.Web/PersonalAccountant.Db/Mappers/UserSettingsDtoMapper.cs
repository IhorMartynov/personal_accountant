using Microsoft.AspNetCore.DataProtection;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

internal sealed class UserSettingsDtoMapper : IUserSettingsDtoMapper
{
	private readonly IDataProtector _dataProtector;

	public UserSettingsDtoMapper(IDataProtectionProvider protectionProvider)
	{
		_dataProtector = protectionProvider.CreateProtector(nameof(UserSettings));
	}


    /// <inheritdoc/>
    public UserSettingsDto Map(UserSettings userSettings)
    {
        if (userSettings == null) throw new ArgumentNullException(nameof(userSettings));

        return new UserSettingsDto(
            userSettings.Name,
            userSettings.Email,
            new GasAccountSettingsDto(
                userSettings.GasAccount.PublicEnterpriseId,
                userSettings.GasAccount.AccountNumber,
                userSettings.GasAccount.Login,
                _dataProtector.Unprotect(userSettings.GasAccount.EncryptedPassword)),
            new ElectricityAccountSettingsDto(
                userSettings.ElectricityAccount.PublicEnterpriseId,
                userSettings.ElectricityAccount.AccountNumber,
                userSettings.ElectricityAccount.Login,
                _dataProtector.Unprotect(userSettings.ElectricityAccount.EncryptedPassword)),
            new WaterAccountSettingsDto(
                userSettings.WaterAccount.PublicEnterpriseId,
                userSettings.WaterAccount.AccountNumber,
                userSettings.WaterAccount.Login,
                _dataProtector.Unprotect(userSettings.WaterAccount.EncryptedPassword)));
    }
}