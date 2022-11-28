﻿using GuildWars2.Raids;
using Xunit;

namespace GuildWars2.Tests.Features.Raids;

internal static class Invariants
{
    internal static void Has_id(this Raid actual) => Assert.NotEmpty(actual.Id);
}
