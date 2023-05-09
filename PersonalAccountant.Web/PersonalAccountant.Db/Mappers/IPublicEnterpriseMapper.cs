using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Mappers;

public interface IPublicEnterpriseMapper
{
    /// <summary>
    /// Maps a PublicEnterpriseDto object to a PublicEnterprise object.
    /// </summary>
    PublicEnterprise Map(PublicEnterpriseDto publicEnterpriseDto);

}