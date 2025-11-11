using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

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
        Assert.NotEmpty(actual);
        Assert.All(actual, space =>
        {
            Assert.NotEmpty(space.Name);
        });
    }
}
