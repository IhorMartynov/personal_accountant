namespace PersonalAccountant.Common.Options;

public sealed class AuthenticationOptions
{
	public GoogleAuthenticationOptions? Google { get; set; }
}

public sealed class GoogleAuthenticationOptions
{
	public string? ClientId { get; set; }
	public string? ClientSecret { get; set; }
}