using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public interface IUserSettingsMapper
{
	/// <summary>
	/// Maps a UserSettingsDto object to a UserSettings object.
	/// </summary>
	/// <param name="userSettings">User Settings DTO object.</param>
	UserSettings Map(UserSettingsDto userSettings);

	/// <summary>
	/// Maps a UserSettingsDto object to a UserSettings object.
	/// </summary>
	/// <param name="id">User Settings entity identifier.</param>
	/// <param name="userSettings">User Settings DTO object.</param>
	UserSettings Map(string? id, UserSettingsDto userSettings);
}