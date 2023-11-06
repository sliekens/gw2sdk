using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class Professions
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Hero.Training.GetProfessions();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);

        Assert.All(
            actual,
            profession =>
            {
                Assert.True(
                    Enum.IsDefined(typeof(ProfessionName), profession.Id),
                    "Enum.IsDefined(profession.Id)"
                );
                Assert.NotEmpty(profession.Name);
                Assert.NotEmpty(profession.Icon);
                Assert.NotEmpty(profession.IconBig);
            }
        );
    }
}
