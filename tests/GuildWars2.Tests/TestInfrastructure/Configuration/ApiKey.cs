namespace GuildWars2.Tests.TestInfrastructure.Configuration;

public sealed record ApiKey
{
    public required string Key { get; init; }
}
