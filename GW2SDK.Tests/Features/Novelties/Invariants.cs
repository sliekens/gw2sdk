﻿using System;
using GuildWars2.Novelties;
using Xunit;

namespace GuildWars2.Tests.Features.Novelties;

internal static class Invariants
{
    internal static void Has_id(this Novelty actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Novelty actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this Novelty actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_icon(this Novelty actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_slot(this Novelty actual) =>
        Assert.True(Enum.IsDefined(typeof(NoveltyKind), actual.Slot));

    internal static void Has_unlock_items(this Novelty actual) =>
        Assert.NotEmpty(actual.UnlockItems);
}
