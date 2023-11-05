using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Professions;

public class ProfessionNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Hero.Professions.GetProfessionNames();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);
        Assert.All(
            actual,
            name => Assert.True(
                Enum.IsDefined(typeof(ProfessionName), name),
                "Enum.IsDefined(name)"
            )
        );
    }
}
