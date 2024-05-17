using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class CompletedPaths
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        // Completed paths reset every day, play some dungeons to test this properly
        var (actual, _) = await sut.Pve.Dungeons.GetCompletedPaths(accessToken.Key);

        Assert.All(actual, entry => Assert.Contains(entry, ReferenceData.Paths));
    }
}
