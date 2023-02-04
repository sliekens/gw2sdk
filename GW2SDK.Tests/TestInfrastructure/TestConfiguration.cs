using System;
using Microsoft.Extensions.Configuration;

namespace GuildWars2.Tests.TestInfrastructure;

public static class TestConfiguration
{
    static TestConfiguration()
    {
        Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .AddUserSecrets(typeof(TestConfiguration).Assembly, true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IConfigurationRoot Configuration { get; }

    public static ApiKeyBasic ApiKeyBasic =>
        new()
        {
            Key = Configuration["ApiKeyBasic"]
                ?? throw new InvalidOperationException("Missing ApiKeyBasic.")
        };

    public static ApiKey ApiKey =>
        new()
        {
            Key = Configuration["ApiKey"]
                ?? throw new InvalidOperationException("Missing ApiKey.")
        };

    public static TestCharacter TestCharacter =>
        new()
        {
            Name =
                Configuration["Character:Name"]
                ?? throw new InvalidOperationException("Missing Character:Name."),
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                Configuration["Character:Race"]
                ?? throw new InvalidOperationException("Missing Character:Race.")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                Configuration["Character:Profession"]
                ?? throw new InvalidOperationException("Missing Character:Profession.")
            )
        };
}

public sealed record TestCharacter
{
    public required string Name { get; init; }

    public required RaceName Race { get; init; }

    public required ProfessionName Profession { get; init; }
}

public sealed record ApiKeyBasic
{
    public required string Key { get; init; }
}

public sealed record ApiKey
{
    public required string Key { get; init; }
}
