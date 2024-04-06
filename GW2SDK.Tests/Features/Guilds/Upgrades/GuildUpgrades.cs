using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgrades
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Guilds.GetGuildUpgrades();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotNull(entry.Name);
                Assert.NotNull(entry.Description);
                Assert.NotEmpty(entry.IconHref);
                Assert.NotNull(entry.Costs);
                if (entry is BankBag bankBag)
                {
                    Assert.True(bankBag.MaxItems > 0);
                    Assert.True(bankBag.MaxCoins > 0);
                }
            }
        );
    }
}
