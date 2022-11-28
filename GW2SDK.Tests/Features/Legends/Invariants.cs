using GuildWars2.Legends;
using Xunit;

namespace GuildWars2.Tests.Features.Legends;

public static class Invariants
{
    internal static void Has_id(this Legend actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_code(this Legend actual) => Assert.True(actual.Code > 0);
}
