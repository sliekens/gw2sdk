using GuildWars2.Wvw.Ranks;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

internal static class Invariants
{
    internal static void Has_id(this Rank actual) => Assert.True(actual.Id > 0);

    internal static void Has_title(this Rank actual) => Assert.NotEmpty(actual.Title);

    internal static void Has_min_rank(this Rank actual) => Assert.True(actual.MinRank > 0);
}
