using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

[ServiceDataSource]
public class UnlockedNodes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey token = TestConfiguration.ApiKey;
        (IImmutableValueSet<string> actual, _) = await sut.Pve.Home.GetUnlockedNodes(token.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (string item in actual)
            {
                await Assert.That(item).IsNotEmpty();
            }
        }
    }
}
