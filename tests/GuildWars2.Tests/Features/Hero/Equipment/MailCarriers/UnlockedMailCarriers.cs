using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

[ServiceDataSource]
public class UnlockedMailCarriers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<int> actual, _) = await sut.Hero.Equipment.MailCarriers.GetUnlockedMailCarriers(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        (IImmutableValueSet<MailCarrier> carriers, _) = await sut.Hero.Equipment.MailCarriers.GetMailCarriersByIds(actual, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(carriers.Count).IsEqualTo(actual.Count);
        foreach (MailCarrier carrier in carriers)
        {
            await Assert.That(actual).Contains(carrier.Id);
        }
    }
}
