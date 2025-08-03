using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Search;

public class GuildsByName
{
    [Fact]
    public async Task Is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        (HashSet<string> actual, _) = await sut.Guilds.GetGuildsByName(
            "ArenaNet",
            TestContext.Current.CancellationToken
        );

        var guild = Assert.Single(actual);

        Assert.Equal("4BBB52AA-D768-4FC6-8EDE-C299F2822F0F", guild);
    }
}
