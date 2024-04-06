using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

public class JadeBotSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.JadeBots.GetJadeBotSkins();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);

                // Missing descriptionfor Roundtail Dragon
                if (entry.Id == 6)
                {
                    Assert.Empty(entry.Description);
                }
                else
                {
                    Assert.NotEmpty(entry.Description);
                }

                Assert.True(entry.UnlockItemId > 0);
            }
        );
    }
}
