using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Quests;

public class QuestById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int questId = 15;

        var actual = await sut.Quests.GetQuestById(questId);

        Assert.Equal(questId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_level();
        actual.Value.Has_story();
        actual.Value.Has_goals();
    }
}
