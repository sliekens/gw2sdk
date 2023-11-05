using GuildWars2.Pvp.MistChampions;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

internal static class Invariants
{
    internal static void Has_id(this MistChampion actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this MistChampion actual) => Assert.NotEmpty(actual.Name);
}
