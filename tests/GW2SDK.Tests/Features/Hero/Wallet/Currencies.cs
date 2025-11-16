using GuildWars2.Hero.Wallet;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Wallet;

[ServiceDataSource]
public class Currencies(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Currency> actual, MessageContext context) = await sut.Hero.Wallet.GetCurrencies(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (Currency currency in actual)
            {
                await Assert.That(currency.Id).IsGreaterThanOrEqualTo(1);
                if (currency.Id == 74)
                {
                    // Unknown currency
                    await Assert.That(currency.Name).IsEmpty();
                    await Assert.That(currency.Description).IsEmpty();
                }
                else
                {
                    await Assert.That(currency.Name).IsNotEmpty();
                    await Assert.That(currency.Description).IsNotEmpty();
                }

                await Assert.That(currency.Order).IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(1000);

                if (currency.IconUrl is not null)
                {
                    await Assert.That(currency.IconUrl.IsAbsoluteUri).IsTrue();
                }
            }
        }
    }
}
