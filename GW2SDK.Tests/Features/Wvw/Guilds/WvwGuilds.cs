using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Wvw.Guilds;

public class WvwGuilds
{
    [Theory]
    [InlineData(WorldRegion.NorthAmerica)]
    [InlineData(WorldRegion.Europe)]
    public async Task Can_be_listed(WorldRegion region)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetWvwGuilds(region);

        Assert.NotNull(context);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotNull(entry.Name);
                Assert.NotEmpty(entry.ShardId);
            }
        );
    }
}
