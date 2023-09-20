using GuildWars2.Pvp.Seasons;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

internal static class Invariants
{
    internal static void Has_id(this Season actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this Season actual) => Assert.NotEmpty(actual.Name);
}
