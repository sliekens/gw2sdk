using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgrades
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<GuildUpgrade> actual, MessageContext context) = await sut.Guilds.GetGuildUpgrades(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotNull(entry.Name);
            Assert.NotNull(entry.Description);
            Assert.True(entry.IconUrl.IsAbsoluteUri);
            Assert.NotNull(entry.Costs);
            if (entry is BankBag bankBag)
            {
                Assert.True(bankBag.MaxItems > 0);
                Assert.True(bankBag.MaxCoins > 0);
            }
        });
    }
}
