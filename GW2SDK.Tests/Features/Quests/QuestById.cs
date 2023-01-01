using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Quests;

public class QuestById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int questId = 15;

        var actual = await sut.Quests.GetQuestById(questId);

        Assert.Equal(questId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_level();
        actual.Value.Has_story();
        actual.Value.Has_goals();
    }
}
