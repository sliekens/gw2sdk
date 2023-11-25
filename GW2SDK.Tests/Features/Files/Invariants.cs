using GuildWars2.Files;

namespace GuildWars2.Tests.Features.Files;

internal static class Invariants
{
    internal static void Has_id(this Asset actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_icon(this Asset actual) => Assert.NotEmpty(actual.IconHref);
}
