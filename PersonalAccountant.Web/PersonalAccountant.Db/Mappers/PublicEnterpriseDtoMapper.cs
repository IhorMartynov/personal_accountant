using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public sealed class PublicEnterpriseDtoMapper : IPublicEnterpriseDtoMapper
{
    /// <inheritdoc />
    public PublicEnterpriseDto Map(PublicEnterprise publicEnterprise)
    {
        if (publicEnterprise == null) throw new ArgumentNullException(nameof(publicEnterprise));

        return new PublicEnterpriseDto(publicEnterprise.Id, publicEnterprise.Name, publicEnterprise.State,
            publicEnterprise.City, publicEnterprise.Type);
    }

    /// <inheritdoc />
    public IEnumerable<PublicEnterpriseDto> Map(IEnumerable<PublicEnterprise> publicEnterprises)
    {
        if (publicEnterprises == null) throw new ArgumentNullException(nameof(publicEnterprises));

        return publicEnterprises.Select(Map).ToList();
    }
}