using PersonalAccountant.Db.Contracts.Models;

namespace PersonalAccountant.Db.Contracts.Repositories;

public interface IPublicEnterprisesRepository
{
    /// <summary>
    /// Get Public Enterprise entity by its id.
    /// </summary>
    /// <param name="id">Public Enterprise identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<PublicEnterpriseDto> GetPublicEnterpriseByIdAsync(string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of Public Enterprises by a state and a city.
    /// </summary>
    /// <param name="state">State</param>
    /// <param name="city">City</param>
    /// <param name="type">Public Enterprise type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<PublicEnterpriseDto>> GetPublicEnterprisesAsync(string state, string city,
        PublicEnterpriseType type, CancellationToken cancellationToken = default);

    /// <summary>
    /// Store Public Enterprise entity to the DB.
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="name">Name</param>
    /// <param name="state">State</param>
    /// <param name="city">City</param>
    /// <param name="type"></param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task AddPublicEnterpriseAsync(string id, string name, string state, string city,
        PublicEnterpriseType type, CancellationToken cancellationToken = default);
}