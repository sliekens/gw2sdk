using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

internal static class Invariants
{
    internal static void IsLink(this LinkHeaderValue actual, string rel, string href)
    {
        Assert.Equal(rel, actual.Rel);
        Assert.Equal(href, actual.Href);
    }
}
