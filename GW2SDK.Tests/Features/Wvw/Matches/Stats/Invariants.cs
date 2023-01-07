using GuildWars2.Wvw.Matches.Stats;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

internal static class Invariants
{
    internal static void Has_id(this MatchStats actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_kills(this MatchStats actual) => Assert.NotNull(actual.Kills);

    internal static void Has_deaths(this MatchStats actual) => Assert.NotNull(actual.Deaths);

    internal static void Has_maps(this MatchStats actual) => Assert.NotEmpty(actual.Maps);
}
