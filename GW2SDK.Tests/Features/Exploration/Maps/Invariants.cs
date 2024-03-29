﻿using GuildWars2.Exploration.Maps;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public static class Invariants
{
    internal static void Has_id(this MapSummary actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this MapSummary actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_id(this Map actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Map actual) => Assert.NotEmpty(actual.Name);
}
