namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Settings;

internal class JwtConfig
{
    public const string SectionName = nameof(JwtConfig);

    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}