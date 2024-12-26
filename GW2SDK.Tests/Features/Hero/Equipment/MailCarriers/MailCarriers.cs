using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

public class MailCarriers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.MailCarriers.GetMailCarriers(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            mailCarrier =>
            {
                Assert.True(mailCarrier.Id >= 1);
                if (mailCarrier.Flags.Default)
                {
                    Assert.Empty(mailCarrier.UnlockItemIds);
                }
                else
                {
                    Assert.NotEmpty(mailCarrier.UnlockItemIds);
                }

                Assert.InRange(mailCarrier.Order, 0, 1000);
                Assert.NotEmpty(mailCarrier.IconHref);
                Assert.NotEmpty(mailCarrier.Name);
            }
        );
    }
}
