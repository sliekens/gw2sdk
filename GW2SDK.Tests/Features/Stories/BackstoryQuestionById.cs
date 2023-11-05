using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class BackstoryQuestionById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 7;

        var (actual, _) = await sut.Stories.GetBackstoryQuestionById(id);

        Assert.Equal(id, actual.Id);
    }
}
