using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Worlds;
using GuildWars2.Wvw.Guilds;

namespace GuildWars2.Tests.Features.Wvw.Guilds;

public class WvwGuilds
{
    [Theory]
    [InlineData(WorldRegion.NorthAmerica)]
    [InlineData(WorldRegion.Europe)]
    public async Task Can_be_listed(WorldRegion region)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<WvwGuild> actual, MessageContext context) = await sut.Wvw.GetWvwGuilds(
            region,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotNull(entry.Name);
                Assert.NotEmpty(entry.TeamId);
            }
        );
    }
}
