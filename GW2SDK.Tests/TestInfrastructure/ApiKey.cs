namespace GuildWars2.Tests.TestInfrastructure;

public sealed record ApiKey
{
    public required string Key { get; init; }
}