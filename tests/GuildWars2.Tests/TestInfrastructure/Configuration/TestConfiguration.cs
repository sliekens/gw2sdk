using GuildWars2.Hero;

using Microsoft.Extensions.Configuration;

using TUnit.Core.Exceptions;

namespace GuildWars2.Tests.TestInfrastructure.Configuration;

#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations

public static class TestConfiguration
{
    [Before(Assembly)]
    public static void SetupTestConfiguration()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(typeof(TestConfiguration).Assembly, true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IConfigurationRoot Configuration { get; set; } = null!;

    public static ApiKeyBasic ApiKeyBasic =>
        new()
        {
            Key = Configuration["ApiKeyBasic"]
                ?? throw new SkipTestException("Missing ApiKeyBasic.")
        };

    public static ApiKey ApiKey =>
        new()
        {
            Key = Configuration["ApiKey"]
                ?? throw new SkipTestException("Missing ApiKey.")
        };

    public static TestCharacter TestCharacter =>
        new()
        {
            Name =
                Configuration["Character:Name"]
                ?? throw new SkipTestException("Missing Character:Name."),
#if NET
            Race = Enum.Parse<RaceName>(
                Configuration["Character:Race"]
                ?? throw new SkipTestException("Missing Character:Race.")
            ),
            Profession = Enum.Parse<ProfessionName>(
                Configuration["Character:Profession"]
                ?? throw new SkipTestException("Missing Character:Profession.")
            )
#else
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                Configuration["Character:Race"]
                ?? throw new SkipTestException("Missing Character:Race.")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                Configuration["Character:Profession"]
                ?? throw new SkipTestException("Missing Character:Profession.")
            )
#endif
        };

    public static TestCharacter TestCharacter2 =>
        new()
        {
            Name =
                Configuration["Character2:Name"]
                ?? throw new SkipTestException("Missing Character2:Name."),
#if NET
            Race = Enum.Parse<RaceName>(
                Configuration["Character2:Race"]
                ?? throw new SkipTestException("Missing Character2:Race.")
            ),
            Profession = Enum.Parse<ProfessionName>(
                Configuration["Character2:Profession"]
                ?? throw new SkipTestException("Missing Character2:Profession.")
            )
#else
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                Configuration["Character2:Race"]
                ?? throw new SkipTestException("Missing Character2:Race.")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                Configuration["Character2:Profession"]
                ?? throw new SkipTestException("Missing Character2:Profession.")
            )
#endif
        };

    public static TestGuild TestGuild =>
        new()
        {
            Name =
                Configuration["Guild:Name"]
                ?? throw new SkipTestException("Missing Guild:Name."),
            Tag = Configuration["Guild:Tag"]
                ?? throw new SkipTestException("Missing Guild:Tag."),
            Id = Configuration["Guild:Id"]
                ?? throw new SkipTestException("Missing Guild:Id.")
        };

    public static TestGuildLeader TestGuildLeader =>
        new()
        {
            Token = Configuration["GuildLeader:Token"]
                ?? throw new SkipTestException("Missing GuildLeader:Token.")
        };
}
