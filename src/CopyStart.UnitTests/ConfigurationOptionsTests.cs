using CopyStart.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CopyStart.UnitTests;

public class ConfigurationOptionsTests
{
    [Fact]
    public void ZitadelOptions_BindsFromConfigurationSection()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"Zitadel:Authority", "https://auth.example.test"},
            {"Zitadel:ClientId", "client-123"},
            {"Zitadel:Audience", "audience-456"},
            {"Zitadel:ProjectId", "project-789"},
            {"Zitadel:RequireHttpsMetadata", "true"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var options = new ZitadelOptions();
        configuration.GetSection(ZitadelOptions.SectionName).Bind(options);

        Assert.Equal("https://auth.example.test", options.Authority);
        Assert.Equal("client-123", options.ClientId);
        Assert.Equal("audience-456", options.Audience);
        Assert.Equal("project-789", options.ProjectId);
        Assert.True(options.RequireHttpsMetadata);
    }

    [Fact]
    public void PostgreSqlOptions_BindsFromConfigurationSection()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"PostgreSQL:ConnectionString", "Host=localhost;Database=copystart_test"},
            {"PostgreSQL:MaxPoolSize", "35"},
            {"PostgreSQL:EnableSensitiveDataLogging", "false"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var options = new PostgreSqlOptions();
        configuration.GetSection(PostgreSqlOptions.SectionName).Bind(options);

        Assert.Equal("Host=localhost;Database=copystart_test", options.ConnectionString);
        Assert.Equal(35, options.MaxPoolSize);
        Assert.False(options.EnableSensitiveDataLogging);
    }

    [Fact]
    public void StorageOptions_BindsFromConfigurationSection()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"Storage:Endpoint", "http://storage.local:8333"},
            {"Storage:ExternalEndpoint", "https://s3.example.test"},
            {"Storage:BucketName", "copystart-files"},
            {"Storage:Region", "us-east-1"},
            {"Storage:ForcePathStyle", "true"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var options = new StorageOptions();
        configuration.GetSection(StorageOptions.SectionName).Bind(options);

        Assert.Equal("http://storage.local:8333", options.Endpoint);
        Assert.Equal("https://s3.example.test", options.ExternalEndpoint);
        Assert.Equal("copystart-files", options.BucketName);
        Assert.Equal("us-east-1", options.Region);
        Assert.True(options.ForcePathStyle);
    }
}
