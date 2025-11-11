namespace GuildWars2.Tests.TestInfrastructure.Configuration;

public sealed record ApiKeyBasic
{
    public required string Key { get; init; }
}
