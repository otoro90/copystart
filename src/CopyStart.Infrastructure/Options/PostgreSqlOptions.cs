namespace CopyStart.Infrastructure.Options;

public sealed class PostgreSqlOptions
{
    public const string SectionName = "PostgreSQL";

    public string ConnectionString { get; set; } = string.Empty;
    public int MaxPoolSize { get; set; } = 20;
    public bool EnableSensitiveDataLogging { get; set; } = false;
}
