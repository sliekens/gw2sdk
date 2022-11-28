using GuildWars2.Pvp.Heroes;
using Xunit;

namespace GuildWars2.Tests.Features.Pvp.Heroes;

internal static class Invariants
{
    internal static void Has_id(this Hero actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this Hero actual) => Assert.NotEmpty(actual.Name);
}
