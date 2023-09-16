using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Skills;

public class SkillById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 61533;

        var actual = await sut.Skills.GetSkillById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
