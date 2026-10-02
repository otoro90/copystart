namespace CopyStart.Infrastructure.Options;

public sealed class StorageOptions
{
    public const string SectionName = "Storage";

    public string Endpoint { get; set; } = string.Empty;
    public string ExternalEndpoint { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string Region { get; set; } = "us-east-1";
    public bool ForcePathStyle { get; set; } = true;
}
