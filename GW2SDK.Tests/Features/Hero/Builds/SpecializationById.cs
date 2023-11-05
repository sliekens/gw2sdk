using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class SpecializationById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Hero.Builds.GetSpecializationById(id);

        Assert.Equal(id, actual.Id);
    }
}
