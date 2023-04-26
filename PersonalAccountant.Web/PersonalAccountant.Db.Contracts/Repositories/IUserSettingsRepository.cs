using PersonalAccountant.Db.Contracts.Models;

namespace PersonalAccountant.Db.Contracts.Repositories;

public interface IUserSettingsRepository
{
	/// <summary>
	/// Retrieves the user settings for the specified email address asynchronously. 
	/// </summary>
	/// <param name="email">User email.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<UserSettingsDto> GetUserSettingsAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>
	/// Saves the user settings asynchronously.
	/// </summary>
	/// <param name="userSettings">The user settings to save.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task SaveUserSettingsAsync(UserSettingsDto userSettings, CancellationToken cancellationToken = default);
}