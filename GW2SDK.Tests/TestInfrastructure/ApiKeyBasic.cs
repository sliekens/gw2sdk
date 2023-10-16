namespace GuildWars2.Tests.TestInfrastructure;

public sealed record ApiKeyBasic
{
    public required string Key { get; init; }
}