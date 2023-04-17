namespace PersonalAccountant.Db.Models;

public sealed class User
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
}