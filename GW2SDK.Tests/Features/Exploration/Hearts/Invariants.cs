using System.Collections.Generic;
using System.Linq;
using GW2SDK.Exploration.Hearts;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Hearts;

public static class Invariants
{
    internal static void All_have_ids(this IReadOnlyCollection<Heart> entries) =>
        Assert.All(entries, actual => Assert.True(actual.Id > 0));

    internal static void Some_have_objectives(this IReadOnlyCollection<Heart> entries)
    {
        var count = entries.Count(actual => !string.IsNullOrEmpty(actual.Objective));
        Assert.InRange(count, 1, entries.Count);
    }

    internal static void All_have_chat_links(this IReadOnlyCollection<Heart> entries) =>
        Assert.All(entries, actual => Assert.NotEmpty(actual.ChatLink));
}
