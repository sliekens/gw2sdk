using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

[ServiceDataSource]
public class UnlockedMailCarriers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.MailCarriers.GetUnlockedMailCarriers(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        (HashSet<MailCarrier> carriers, _) = await sut.Hero.Equipment.MailCarriers.GetMailCarriersByIds(actual, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(actual.Count, carriers.Count);
        Assert.All(carriers, carrier => Assert.Contains(carrier.Id, actual));
    }
}
