using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class TitleById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Achievements.GetTitleById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Can_be_unlocked_by_achievements();
    }
}
