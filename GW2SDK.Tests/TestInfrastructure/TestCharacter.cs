using GuildWars2.Hero;

namespace GuildWars2.Tests.TestInfrastructure;

public sealed record TestCharacter
{
    public required string Name { get; init; }

    public required RaceName Race { get; init; }

    public required ProfessionName Profession { get; init; }
}