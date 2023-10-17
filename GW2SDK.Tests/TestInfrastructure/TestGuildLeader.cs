namespace GuildWars2.Tests.TestInfrastructure;

public sealed record TestGuildLeader
{
    public required string Id { get; init; }

    public required string Token { get; init; }
}
