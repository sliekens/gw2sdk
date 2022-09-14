﻿using GW2SDK.Emotes;
using Xunit;

namespace GW2SDK.Tests.Features.Emotes;

internal static class Invariants
{
    internal static void Id_is_not_empty(this Emote actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_commands(this Emote actual) => Assert.NotEmpty(actual.Commands);

    internal static void Has_unlock_items(this Emote actual) => Assert.NotEmpty(actual.UnlockItems);
}