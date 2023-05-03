using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public sealed class PublicEnterpriseMapper : IPublicEnterpriseMapper
{
    /// <inheritdoc />
    public PublicEnterprise Map(PublicEnterpriseDto publicEnterpriseDto) =>
        new(publicEnterpriseDto.Id, publicEnterpriseDto.Name, publicEnterpriseDto.State,
            publicEnterpriseDto.City, publicEnterpriseDto.Type);
}