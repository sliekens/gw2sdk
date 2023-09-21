using GuildWars2.Pvp.Games;

namespace GuildWars2.Tests.Features.Pvp.Games;

internal static class Invariants
{
    internal static void Has_id(this Game actual) => Assert.NotEmpty(actual.Id);
}
