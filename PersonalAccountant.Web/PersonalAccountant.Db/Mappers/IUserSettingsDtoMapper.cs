using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public interface IUserSettingsDtoMapper
{
	/// <summary>
	/// Maps a UserSettings object to a UserSettingsDto object.
	/// </summary>
	UserSettingsDto Map(UserSettings userSettings);
}