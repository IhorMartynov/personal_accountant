using PersonalAccountant.Db.Contracts.Models;

namespace PersonalAccountant.Db.Models;

public sealed record PublicEnterprise(string Id, string Name, string State, string City, PublicEnterpriseType Type);