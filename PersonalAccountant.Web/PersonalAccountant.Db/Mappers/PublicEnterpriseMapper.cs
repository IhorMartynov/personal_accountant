using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

internal sealed class PublicEnterpriseMapper : IPublicEnterpriseMapper
{
    /// <inheritdoc />
    public PublicEnterprise Map(PublicEnterpriseDto publicEnterpriseDto)
    {
        if (publicEnterpriseDto == null) throw new ArgumentNullException(nameof(publicEnterpriseDto));

        return new PublicEnterprise(publicEnterpriseDto.Id, publicEnterpriseDto.Name, publicEnterpriseDto.State,
            publicEnterpriseDto.City, publicEnterpriseDto.Type);
    }
}