using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Search;

[ServiceDataSource]
public class GuildsByName(Gw2Client sut)
{
    [Test]
    public async Task Is_not_empty()
    {
        (HashSet<string> actual, _) = await sut.Guilds.GetGuildsByName("ArenaNet", TestContext.Current!.Execution.CancellationToken);
        string? guild = Assert.Single(actual);
        Assert.Equal("4BBB52AA-D768-4FC6-8EDE-C299F2822F0F", guild);
    }
}
