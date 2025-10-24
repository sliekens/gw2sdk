using GuildWars2.Hero.Wallet;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Wallet;

public class CurrencyById
{

    [Test]

    public async Task A_currency_can_be_found_by_id()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (Currency actual, MessageContext context) = await sut.Hero.Wallet.GetCurrencyById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
