using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Search;

[ServiceDataSource]
public class GuildsByName(Gw2Client sut)
{
    [Test]
    public async Task Is_not_empty()
    {
        (IImmutableValueSet<string> actual, _) = await sut.Guilds.GetGuildsByName("ArenaNet", TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).HasSingleItem();
        string? guild = actual.Single();
        await Assert.That(guild).IsEqualTo("4BBB52AA-D768-4FC6-8EDE-C299F2822F0F");
    }
}
