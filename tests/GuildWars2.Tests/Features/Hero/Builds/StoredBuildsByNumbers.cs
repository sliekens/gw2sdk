using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class StoredBuildsByNumbers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        HashSet<int> slotNumbers = [2, 3, 4];
        ;
        (IReadOnlyList<GuildWars2.Hero.Builds.Build> actual, _) = await sut.Hero.Builds.GetStoredBuilds(slotNumbers, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (GuildWars2.Hero.Builds.Build space in actual)
        {
            await Assert.That(space.Name).IsNotEmpty();
        }
    }
}
