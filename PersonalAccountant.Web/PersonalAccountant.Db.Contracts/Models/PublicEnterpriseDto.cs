namespace PersonalAccountant.Db.Contracts.Models;

public sealed record PublicEnterpriseDto(string Id, string Name, string State, string City, PublicEnterpriseType Type);
