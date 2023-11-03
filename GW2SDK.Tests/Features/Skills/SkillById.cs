using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Skills;

public class SkillById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 61533;

        var actual = await sut.Builds.GetSkillById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
