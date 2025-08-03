using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

public class MailCarrierById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (MailCarrier actual, MessageContext context) = await sut.Hero.Equipment.MailCarriers.GetMailCarrierById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
