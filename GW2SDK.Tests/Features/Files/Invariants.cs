using GuildWars2.Files;
using Xunit;

namespace GuildWars2.Tests.Features.Files;

internal static class Invariants
{
    internal static void Has_id(this File actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_icon(this File actual) => Assert.NotEmpty(actual.Icon);
}
