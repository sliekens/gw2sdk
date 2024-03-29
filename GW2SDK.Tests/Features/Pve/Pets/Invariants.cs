﻿using GuildWars2.Pve.Pets;

namespace GuildWars2.Tests.Features.Pve.Pets;

internal static class Invariants
{
    internal static void Has_id(this Pet actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Pet actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this Pet actual) => Assert.NotEmpty(actual.Description);

    internal static void Has_icon(this Pet actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_skills(this Pet actual) => Assert.NotEmpty(actual.Skills);
}
