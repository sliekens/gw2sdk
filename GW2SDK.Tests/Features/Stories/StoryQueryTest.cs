using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Stories;

public class StoryQueryTest
{

    [Fact]
    public async Task Backstory_questions_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryQuestions();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);

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

    [Fact]
    public async Task Backstory_answers_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryAnswers();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);

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

    [Fact]
    public async Task Backstory_questions_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryQuestionsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task Backstory_answers_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryAnswersIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_backstory_question_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 7;

        var actual = await sut.Stories.GetBackstoryQuestionById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task A_backstory_answer_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "7-53";

        var actual = await sut.Stories.GetBackstoryAnswerById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Backstory_questions_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            7,
            10,
            11
        };

        var actual = await sut.Stories.GetBackstoryQuestionsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Backstory_answers_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "7-53",
            "7-54",
            "7-55"
        };

        var actual = await sut.Stories.GetBackstoryAnswersByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Backstory_questions_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryQuestionsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Backstory_answers_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryAnswersByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
