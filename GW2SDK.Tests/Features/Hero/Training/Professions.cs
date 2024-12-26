using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class Professions
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Hero.Training.GetProfessions(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);

        Assert.All(
            actual,
            profession =>
            {
                Assert.True(profession.Id.IsDefined());
                Assert.NotEmpty(profession.Name);
                Assert.NotEmpty(profession.IconHref);
                Assert.NotEmpty(profession.BigIconHref);
            }
        );
    }
}
