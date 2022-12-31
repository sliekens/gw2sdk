using GuildWars2.Wvw.Matches.Scores;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

internal static class Invariants
{
    internal static void Has_id(this MatchScores actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_scores(this MatchScores actual) => Assert.NotNull(actual.Scores);

    internal static void Has_victory_points(this MatchScores actual) =>
        Assert.NotNull(actual.VictoryPoints);

    internal static void Has_skirmishes(this MatchScores actual) =>
        Assert.NotNull(actual.Skirmishes);
}
