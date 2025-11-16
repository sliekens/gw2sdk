using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Worlds;
using GuildWars2.Wvw.Guilds;

namespace GuildWars2.Tests.Features.Wvw.Guilds;

[ServiceDataSource]
public class WvwGuilds(Gw2Client sut)
{
    [Test]
    [Arguments(WorldRegion.NorthAmerica)]
    [Arguments(WorldRegion.Europe)]
    public async Task Can_be_listed(WorldRegion region)
    {
        (HashSet<WvwGuild> actual, MessageContext context) = await sut.Wvw.GetWvwGuilds(region, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual).IsNotEmpty();
        foreach (WvwGuild entry in actual)
        {
            await Assert.That(entry.Name).IsNotNull();
            await Assert.That(entry.TeamId).IsNotEmpty();
        }
    }
}
