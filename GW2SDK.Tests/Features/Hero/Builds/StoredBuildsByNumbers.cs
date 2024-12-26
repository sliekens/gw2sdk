using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuildsByNumbers
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        HashSet<int> slotNumbers =
        [
            2,
            3,
            4
        ];
        ;

        var (actual, _) = await sut.Hero.Builds.GetStoredBuilds(slotNumbers, accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            space =>
            {
                Assert.NotEmpty(space.Name);
            }
        );
    }
}
