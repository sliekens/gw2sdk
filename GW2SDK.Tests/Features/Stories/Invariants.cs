using GuildWars2.Stories;
using Xunit;

namespace GuildWars2.Tests.Features.Stories;

internal static class Invariants
{
    internal static void Has_id(this Story actual) => Assert.True(actual.Id > 0);

    internal static void Has_id(this Season actual) => Assert.NotEmpty(actual.Id);
}
