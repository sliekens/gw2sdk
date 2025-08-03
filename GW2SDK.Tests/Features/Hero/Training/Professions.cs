using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class Professions
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Profession> actual, _) = await sut.Hero.Training.GetProfessions(
            cancellationToken: TestContext.Current.CancellationToken
        );
#if NET
        Assert.Equal(Enum.GetNames<ProfessionName>().Length, actual.Count);
#else
        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);
#endif
        Assert.All(
            actual,
            profession =>
            {
                Assert.True(profession.Id.IsDefined());
                Assert.NotEmpty(profession.Name);
                Assert.True(profession.IconUrl is null || profession.IconUrl.IsAbsoluteUri);
                Assert.True(profession.BigIconUrl is null || profession.BigIconUrl.IsAbsoluteUri);
            }
        );
    }
}
