using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

[ServiceDataSource]
public class UnlockedDecorations(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey token = TestConfiguration.ApiKey;
        (HashSet<UnlockedDecoration> actual, _) = await sut.Pve.Home.GetUnlockedDecorations(token.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (UnlockedDecoration entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry.Count > 0).IsTrue();
        }
    }
}
