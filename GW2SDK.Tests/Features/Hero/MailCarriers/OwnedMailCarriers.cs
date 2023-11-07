using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.MailCarriers;

public class OwnedMailCarriers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.MailCarriers.GetOwnedMailCarriers(accessToken.Key);

        Assert.NotEmpty(actual);

        var (carriers, _) = await sut.Hero.MailCarriers.GetMailCarriersByIds(actual);

        Assert.Equal(actual.Count, carriers.Count);
        Assert.All(carriers, carrier => Assert.Contains(carrier.Id, actual));
    }
}
