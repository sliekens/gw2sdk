using GuildWars2.Http.Headers;

namespace GuildWars2.Tests.Http.Headers;

internal static class Invariants
{
    internal static void IsLink(this LinkValue actual, string relationType, string target)
    {
        Assert.Equal(relationType, actual.RelationType);
        Assert.Equal(target, actual.Target);
    }
}
