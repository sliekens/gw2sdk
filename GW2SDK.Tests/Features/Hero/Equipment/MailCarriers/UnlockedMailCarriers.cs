using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

public class UnlockedMailCarriers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) =
            await sut.Hero.Equipment.MailCarriers.GetUnlockedMailCarriers(accessToken.Key);

        Assert.NotEmpty(actual);

        var (carriers, _) = await sut.Hero.Equipment.MailCarriers.GetMailCarriersByIds(actual);

        Assert.Equal(actual.Count, carriers.Count);
        Assert.All(carriers, carrier => Assert.Contains(carrier.Id, actual));
    }
}
