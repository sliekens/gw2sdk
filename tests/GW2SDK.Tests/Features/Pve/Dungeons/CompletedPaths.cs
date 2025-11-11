using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

[ServiceDataSource]
public class CompletedPaths(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        // Completed paths reset every day, play some dungeons to test this properly
        (HashSet<string> actual, _) = await sut.Pve.Dungeons.GetCompletedPaths(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.All(actual, entry => Assert.Contains(entry, ReferenceData.Paths));
    }
}
