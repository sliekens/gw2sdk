﻿using GuildWars2.Exploration.PointsOfInterest;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public static class Invariants
{
    internal static void All_have_ids(this IReadOnlyCollection<PointOfInterest> entries) =>
        Assert.All(entries, actual => Assert.True(actual.Id > 0));

    internal static void Some_have_names(this IReadOnlyCollection<PointOfInterest> entries)
    {
        var count = entries.Count(actual => !string.IsNullOrEmpty(actual.Name));
        Assert.InRange(count, 1, entries.Count);
    }

    internal static void All_have_chat_links(this IReadOnlyCollection<PointOfInterest> entries) =>
        Assert.All(
            entries,
            actual =>
            {
                var chatLink = actual.GetChatLink();
                Assert.Equal(actual.Id, chatLink.PointOfInterestId);
                Assert.Equal(actual.ChatLink, chatLink.ToString());
            }
        );
}
