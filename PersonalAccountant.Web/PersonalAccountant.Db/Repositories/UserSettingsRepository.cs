using MongoDB.Driver;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Contracts.Repositories;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Repositories;

public sealed class UserSettingsRepository : IUserSettingsRepository
{
    private const string UserSettingsCollectionName = nameof(UserSettings);

    private readonly IMongoDatabase _database;
    private readonly IUserSettingsDtoMapper _userSettingsDtoMapper;
    private readonly IUserSettingsMapper _userSettingsMapper;

    public UserSettingsRepository(IMongoDatabase database,
	    IUserSettingsDtoMapper userSettingsDtoMapper,
	    IUserSettingsMapper userSettingsMapper)
    {
	    _database = database;
	    _userSettingsDtoMapper = userSettingsDtoMapper;
	    _userSettingsMapper = userSettingsMapper;
    }

    /// <inheritdoc/>
    public async Task<UserSettingsDto> GetUserSettingsAsync(string email, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<UserSettings>(UserSettingsCollectionName);

        var userSettings = await collection.Find(x =>
		        x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
	        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return _userSettingsDtoMapper.Map(userSettings);
    }

    /// <inheritdoc/>
    public async Task SaveUserSettingsAsync(UserSettingsDto userSettings, CancellationToken cancellationToken = default)
    {
	    var collection = _database.GetCollection<UserSettings>(UserSettingsCollectionName);

	    var userSettingsToUpdate = _userSettingsMapper.Map(null, userSettings);

	    var updatedDocument = await collection.FindOneAndUpdateAsync(x =>
			    x.Email.Equals(userSettings.Email, StringComparison.InvariantCultureIgnoreCase),
		    new ObjectUpdateDefinition<UserSettings>(userSettingsToUpdate),
		    new() {ReturnDocument = ReturnDocument.After},
		    cancellationToken);
	
	    if (updatedDocument is null)
	    {
		    await collection.InsertOneAsync(_userSettingsMapper.Map(userSettings), null,
			    cancellationToken);
	    }
    }
}