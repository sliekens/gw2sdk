﻿using GuildWars2.Wvw.Objectives;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

internal static class Invariants
{
    internal static void Has_id(this Objective actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this Objective actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_sector_id(this Objective actual) => Assert.True(actual.SectorId > 0);

    internal static void Has_map_id(this Objective actual) => Assert.True(actual.MapId > 0);

    internal static void Has_chat_link(this Objective actual) => Assert.NotEmpty(actual.ChatLink);
}
