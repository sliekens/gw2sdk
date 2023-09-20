using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.BuildStorage;

public class BuildStorage
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.BuildStorage.GetBuildStorage(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
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
