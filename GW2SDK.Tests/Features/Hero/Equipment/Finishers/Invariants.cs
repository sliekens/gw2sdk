﻿using GuildWars2.Hero.Equipment.Finishers;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

internal static class Invariants
{
    internal static void Has_id(this Finisher actual) => Assert.True(actual.Id > 0);

    internal static void Has_unlock_details(this Finisher actual) =>
        Assert.NotNull(actual.LockedText);

    internal static void Has_unlock_items(this Finisher actual) =>
        Assert.NotNull(actual.UnlockItemIds);

    internal static void Has_order(this Finisher actual) => Assert.True(actual.Order >= 0);

    internal static void Has_icon(this Finisher actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_name(this Finisher actual) => Assert.NotEmpty(actual.Name);
}
