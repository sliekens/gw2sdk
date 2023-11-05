using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class StoredBuildsByNumbers
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        HashSet<int> slotNumbers = new()
        {
            2,
            3,
            4
        };
        ;

        var (actual, _) = await sut.Builds.GetStoredBuilds(slotNumbers, accessToken.Key);

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
