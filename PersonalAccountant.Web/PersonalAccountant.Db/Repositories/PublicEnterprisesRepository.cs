using MongoDB.Driver;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Contracts.Repositories;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Repositories;

public sealed class PublicEnterprisesRepository : IPublicEnterprisesRepository
{
    private const string PublicEnterprisesCollectionName = nameof(PublicEnterprise);

    private readonly IMongoDatabase _database;
    private readonly IPublicEnterpriseDtoMapper _publicEnterpriseDtoMapper;

    public PublicEnterprisesRepository(
        IMongoDatabase database,
        IPublicEnterpriseDtoMapper publicEnterpriseDtoMapper)
    {
        _database = database;
        _publicEnterpriseDtoMapper = publicEnterpriseDtoMapper;
    }

    /// <inheritdoc />
    public async Task<PublicEnterpriseDto> GetPublicEnterpriseByIdAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<PublicEnterprise>(PublicEnterprisesCollectionName);

        var item = await collection.Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return _publicEnterpriseDtoMapper.Map(item);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PublicEnterpriseDto>> GetPublicEnterprisesAsync(string state, string city,
        PublicEnterpriseType type, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<PublicEnterprise>(PublicEnterprisesCollectionName);

        var items = await collection
            .Find(x => x.Type == type
                       && x.State.Equals(state, StringComparison.InvariantCultureIgnoreCase)
                       && x.City.Equals(city, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync(cancellationToken: cancellationToken);

        return _publicEnterpriseDtoMapper.Map(items);
    }

    /// <inheritdoc />
    public Task AddPublicEnterpriseAsync(string id, string name, string state, string city,
        PublicEnterpriseType type, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<PublicEnterprise>(PublicEnterprisesCollectionName);

        var item = new PublicEnterprise(id, name, state, city, type);

        return collection.InsertOneAsync(item, cancellationToken: cancellationToken);
    }
}