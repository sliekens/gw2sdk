using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.MailCarriers;

public class OwnedMailCarriers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.MailCarriers.GetOwnedMailCarriers(accessToken.Key);

        Assert.NotEmpty(actual.Value);

        var carriers = await sut.MailCarriers.GetMailCarriersByIds(actual.Value);

        Assert.Equal(actual.Value.Count, carriers.Value.Count);
        Assert.All(carriers.Value, carrier => Assert.Contains(carrier.Id, actual.Value));
    }
}
