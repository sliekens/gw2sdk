using System;
using System.Linq;
using GuildWars2.Wvw.Matches;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches;

internal static class Invariants
{
    internal static void Has_id(this Match actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_start_time(this Match actual) =>
        Assert.True(actual.StartTime > DateTimeOffset.MinValue);

    internal static void Has_end_time(this Match actual) =>
        Assert.True(actual.EndTime > actual.StartTime);

    internal static void Includes_world(this Match actual, int worldId)
    {
        var all = actual.AllWorlds.Blue.ToList();
        all.AddRange(actual.AllWorlds.Green);
        all.AddRange(actual.AllWorlds.Red);
        Assert.Contains(worldId, all);
    }
}
