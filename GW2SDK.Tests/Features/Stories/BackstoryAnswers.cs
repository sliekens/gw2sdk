using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class BackstoryAnswers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryAnswers();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);

        Assert.All(
            actual.Value,
            answer =>
            {
                answer.Id_is_not_empty();
                answer.Title_is_not_null();
                answer.Description_is_not_empty();
                answer.Journal_is_not_empty();
                answer.Has_a_question();
            }
        );
    }
}
