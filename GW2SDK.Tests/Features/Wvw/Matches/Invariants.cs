using System;
using GuildWars2.Wvw.Matches;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches;

internal static class Invariants
{
    internal static void Has_id(this Match actual) => Assert.NotEmpty(actual.Id);

    internal static void has_start_time(this Match actual) => Assert.True(actual.StartTime > DateTimeOffset.MinValue);

    internal static void Has_end_time(this Match actual) => Assert.True(actual.EndTime > actual.StartTime);
}
