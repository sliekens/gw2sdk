using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class CompletedPaths
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        // Completed paths reset every day, play some dungeons to test this properly
        (HashSet<string> actual, _) = await sut.Pve.Dungeons.GetCompletedPaths(accessToken.Key, TestContext.Current!.CancellationToken);
        Assert.All(actual, entry => Assert.Contains(entry, ReferenceData.Paths));
    }
}
