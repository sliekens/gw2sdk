using System;
using Microsoft.Extensions.Configuration;

namespace GuildWars2.Tests.TestInfrastructure;

public class ConfigurationManager
{
    public ConfigurationManager()
    {
        Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .AddUserSecrets<ConfigurationManager>(true)
            .AddEnvironmentVariables()
            .Build();
    }

    private IConfigurationRoot Configuration { get; }

    public string ApiKeyBasic =>
        Configuration["ApiKeyBasic"] ?? throw new InvalidOperationException("Missing ApiKeyBasic.");

    public string ApiKey =>
        Configuration["ApiKey"] ?? throw new InvalidOperationException("Missing ApiKey.");

    public string CharacterName =>
        Configuration["CharacterName"]
        ?? throw new InvalidOperationException("Missing CharacterName.");
}

public sealed class TestCharacterName
{
    public TestCharacterName(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

public sealed class ApiKeyBasic
{
    public ApiKeyBasic(string key)
    {
        Key = key;
    }

    public string Key { get; }
}

public sealed class ApiKey
{
    public ApiKey(string key)
    {
        Key = key;
    }

    public string Key { get; }
}
