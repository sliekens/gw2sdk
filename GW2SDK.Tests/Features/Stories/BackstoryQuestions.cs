using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class BackstoryQuestions
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryQuestions();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);

        Assert.All(
            actual.Value,
            question =>
            {
                question.Id_is_positive();
                question.Title_is_not_null();
                question.Description_is_not_empty();
                question.Has_3_to_8_answers();
            }
        );
    }
}
