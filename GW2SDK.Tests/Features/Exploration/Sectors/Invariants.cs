using System.Collections.Generic;
using System.Linq;
using GuildWars2.Exploration.Sectors;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public static class Invariants
{
    internal static void All_have_ids(this IReadOnlyCollection<Sector> entries) =>
        Assert.All(entries, actual => Assert.True(actual.Id > 0));

    internal static void Some_have_names(this IReadOnlyCollection<Sector> entries)
    {
        var count = entries.Count(actual => !string.IsNullOrEmpty(actual.Name));
        Assert.InRange(count, 1, entries.Count);
    }

    internal static void All_have_chat_links(this IReadOnlyCollection<Sector> entries) =>
        Assert.All(entries, actual => Assert.NotEmpty(actual.ChatLink));
}
