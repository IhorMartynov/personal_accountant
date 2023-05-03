using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Services;

internal sealed class ConfigureMongoDbIndexesService : IHostedService
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<ConfigureMongoDbIndexesService> _logger;

    public ConfigureMongoDbIndexesService(IMongoDatabase database,
        ILogger<ConfigureMongoDbIndexesService> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await CreateUserSettingsCollectionIndices(cancellationToken);
        await CreatePublicEnterprisesCollectionIndices(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;

    private async Task CreateUserSettingsCollectionIndices(CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<UserSettings>(nameof(UserSettings));

        await CreateSingleAscendingIndex(collection, x => x.Email, cancellationToken);
    }

    private async Task CreatePublicEnterprisesCollectionIndices(CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<PublicEnterprise>(nameof(PublicEnterprise));

        await CreateSingleAscendingIndex(collection, x => x.Name, cancellationToken);
        await CreateCompoundAscendingIndex(collection, cancellationToken, x => x.Type, x => x.State, x => x.City);
    }

    private async Task CreateSingleAscendingIndex<TEntity>(IMongoCollection<TEntity> collection,
        Expression<Func<TEntity, object>> getIndexField,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        _logger.LogDebug("Creating single ascending index for the {0} collection ({1}).", nameof(TEntity),
            getIndexField.ToString());

        var indexKeyDefinitions = Builders<TEntity>.IndexKeys.Ascending(getIndexField);
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<TEntity>(indexKeyDefinitions),
            cancellationToken: cancellationToken);
    }

    private async Task CreateCompoundAscendingIndex<TEntity>(IMongoCollection<TEntity> collection,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] getIndexField)
        where TEntity : class
    {
        var fieldsStringBuilder = new StringBuilder(getIndexField[0].ToString());

        var indexKeyDefinitions = Builders<TEntity>.IndexKeys.Ascending(getIndexField[0]);
        for (var i = 1; i < getIndexField.Length; i++)
        {
            fieldsStringBuilder.Append(", ");
            fieldsStringBuilder.Append(getIndexField[i].ToString());
            indexKeyDefinitions = indexKeyDefinitions.Ascending(getIndexField[i]);
        }

        _logger.LogDebug("Creating compound index for the {0} collection ({1}).", nameof(TEntity),
            fieldsStringBuilder.ToString());
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<TEntity>(indexKeyDefinitions),
            cancellationToken: cancellationToken);
    }
}