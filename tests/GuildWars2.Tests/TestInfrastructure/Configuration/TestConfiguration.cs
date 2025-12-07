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

    private static string GetRequired(string key)
    {
        string? value = Configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new SkipTestException("Missing " + key + ".");
        }
        return value!;
    }

    public static ApiKeyBasic ApiKeyBasic =>
        new()
        {
            Key = GetRequired("ApiKeyBasic")
        };

    public static ApiKey ApiKey =>
        new()
        {
            Key = GetRequired("ApiKey")
        };

    public static TestCharacter TestCharacter =>
        new()
        {
            Name = GetRequired("Character:Name"),
#if NET
            Race = Enum.Parse<RaceName>(GetRequired("Character:Race")),
            Profession = Enum.Parse<ProfessionName>(GetRequired("Character:Profession"))
#else
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                GetRequired("Character:Race")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                GetRequired("Character:Profession")
            )
#endif
        };

    public static TestCharacter TestCharacter2 =>
        new()
        {
            Name = GetRequired("Character2:Name"),
#if NET
            Race = Enum.Parse<RaceName>(GetRequired("Character2:Race")),
            Profession = Enum.Parse<ProfessionName>(GetRequired("Character2:Profession"))
#else
            Race = (RaceName)Enum.Parse(
                typeof(RaceName),
                GetRequired("Character2:Race")
            ),
            Profession = (ProfessionName)Enum.Parse(
                typeof(ProfessionName),
                GetRequired("Character2:Profession")
            )
#endif
        };

    public static TestGuild TestGuild =>
        new()
        {
            Name = GetRequired("Guild:Name"),
            Tag = GetRequired("Guild:Tag"),
            Id = GetRequired("Guild:Id")
        };

    public static TestGuildLeader TestGuildLeader =>
        new()
        {
            Token = GetRequired("GuildLeader:Token")
        };
}
