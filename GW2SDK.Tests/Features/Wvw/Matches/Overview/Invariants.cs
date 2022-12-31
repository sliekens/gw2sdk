using System;
using System.Linq;
using GuildWars2.Wvw.Matches.Overview;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

internal static class Invariants
{
    internal static void Has_id(this MatchOverview actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_start_time(this MatchOverview actual) =>
        Assert.True(actual.StartTime > DateTimeOffset.MinValue);

    internal static void Has_end_time(this MatchOverview actual) =>
        Assert.True(actual.EndTime > actual.StartTime);

    internal static void Includes_world(this MatchOverview actual, int worldId)
    {
        var all = actual.AllWorlds.Blue.ToList();
        all.AddRange(actual.AllWorlds.Green);
        all.AddRange(actual.AllWorlds.Red);
        Assert.Contains(worldId, all);
    }
}
