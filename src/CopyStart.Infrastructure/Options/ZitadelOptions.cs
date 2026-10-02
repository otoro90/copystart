namespace CopyStart.Infrastructure.Options;

public sealed class ZitadelOptions
{
    public const string SectionName = "Zitadel";

    public string Authority { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = true;
}
