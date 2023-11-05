using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class StoredBuilds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Builds.GetStoredBuilds(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            space =>
            {
                Assert.NotNull(space.Name);
                Assert.True(
                    Enum.IsDefined(typeof(ProfessionName), space.Profession),
                    "Enum.IsDefined(space.Profession)"
                );
                Assert.Equal(3, space.Specializations.Count);
                Assert.NotNull(space.Skills);
                Assert.NotNull(space.AquaticSkills);
            }
        );
    }
}
