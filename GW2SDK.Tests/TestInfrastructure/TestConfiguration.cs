using GuildWars2.Hero;
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

    public static TestCharacter TestCharacter2 =>
        new()
        {
            Name =
                Configuration["Character2:Name"]
                ?? throw new InvalidOperationException("Missing Character2:Name."),
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                Configuration["Character2:Race"]
                ?? throw new InvalidOperationException("Missing Character2:Race.")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                Configuration["Character2:Profession"]
                ?? throw new InvalidOperationException("Missing Character2:Profession.")
            )
        };

    public static TestGuild TestGuild =>
        new()
        {
            Name =
                Configuration["Guild:Name"]
                ?? throw new InvalidOperationException("Missing Guild:Name."),
            Tag = Configuration["Guild:Tag"]
                ?? throw new InvalidOperationException("Missing Guild:Tag."),
            Id = Configuration["Guild:Id"]
                ?? throw new InvalidOperationException("Missing Guild:Id.")
        };

    public static TestGuildLeader TestGuildLeader =>
        new()
        {
            Token = Configuration["GuildLeader:Token"]
                ?? throw new InvalidOperationException(
                    "$XunitDynamicSkip$Missing GuildLeader:Token."
                )
        };
}
