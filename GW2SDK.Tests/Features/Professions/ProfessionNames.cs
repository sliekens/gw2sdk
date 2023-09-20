using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Professions;

public class ProfessionNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessionNames();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Value.Count);
        Assert.All(
            actual.Value,
            name => Assert.True(
                Enum.IsDefined(typeof(ProfessionName), name),
                "Enum.IsDefined(name)"
            )
        );
    }
}
