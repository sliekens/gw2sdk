namespace GuildWars2.Tests.TestInfrastructure;

public sealed record TestGuild
{
    public required string Name { get; init; }

    public required string Tag { get; init; }

    public required string Id { get; init; }
}
