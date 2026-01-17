using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

[ServiceDataSource]
public class UnlockedGliderSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<int> actual, _) = await sut.Hero.Equipment.Gliders.GetUnlockedGliderSkins(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (int entry in actual)
        {
            await Assert.That(entry > 0).IsTrue();
        }
    }
}
