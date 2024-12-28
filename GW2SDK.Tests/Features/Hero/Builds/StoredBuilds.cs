using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuilds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Builds.GetStoredBuilds(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            space =>
            {
                Assert.NotNull(space.Name);
                Assert.True(space.Profession.IsDefined());
                Assert.True(space.Specialization1?.Id is null or > 0);
                Assert.True(space.Specialization2?.Id is null or > 0);
                Assert.True(space.Specialization3?.Id is null or > 0);
                Assert.NotNull(space.Skills);
                Assert.NotNull(space.AquaticSkills);
            }
        );
    }
}
