using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public interface IPublicEnterpriseDtoMapper
{
    /// <summary>
    /// Maps a PublicEnterprise object to a PublicEnterpriseDto object.
    /// </summary>
    PublicEnterpriseDto Map(PublicEnterprise publicEnterprise);

    /// <summary>
    /// Maps a PublicEnterprise objects to a PublicEnterpriseDto objects.
    /// </summary>
    IEnumerable<PublicEnterpriseDto> Map(IEnumerable<PublicEnterprise> publicEnterprises);
}