using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Stories;
using GW2SDK.Stories.Models;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Stories;

public class StoryJournalTest
{
    private static class BackstoryQuestionFact
    {
        public static void Id_is_positive(BackstoryQuestion actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Title_is_not_null(BackstoryQuestion actual) => Assert.NotNull(actual.Title);

        public static void Description_is_not_empty(BackstoryQuestion actual) => Assert.NotEmpty(actual.Description);

        public static void Has_3_to_8_answers(BackstoryQuestion actual) => Assert.InRange(actual.Answers.Count, 3, 8);
    }

    private static class BackstoryAnswerFact
    {
        public static void Id_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Id);

        public static void Title_is_not_null(BackstoryAnswer actual) => Assert.NotNull(actual.Title);

        public static void Description_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Description);

        public static void Journal_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Journal);

        public static void Has_a_question(BackstoryAnswer actual) => Assert.InRange(actual.Question, 1, 999);
    }

    [Fact]
    public async Task It_can_get_all_backstory_questions()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryQuestions();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);

        Assert.All(actual,
            question =>
            {
                BackstoryQuestionFact.Id_is_positive(question);
                BackstoryQuestionFact.Title_is_not_null(question);
                BackstoryQuestionFact.Description_is_not_empty(question);
                BackstoryQuestionFact.Has_3_to_8_answers(question);
            });
    }

    [Fact]
    public async Task It_can_get_all_backstory_answers()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryAnswers();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);

        Assert.All(actual,
            answer =>
            {
                BackstoryAnswerFact.Id_is_not_empty(answer);
                BackstoryAnswerFact.Title_is_not_null(answer);
                BackstoryAnswerFact.Description_is_not_empty(answer);
                BackstoryAnswerFact.Journal_is_not_empty(answer);
                BackstoryAnswerFact.Has_a_question(answer);
            });
    }

    [Fact]
    public async Task It_can_get_all_backstory_question_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryQuestionsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_all_backstory_answer_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryAnswersIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_backstory_question_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        const int questionId = 7;

        var actual = await sut.GetBackstoryQuestionById(questionId);

        Assert.Equal(questionId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_a_backstory_answer_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        const string answerId = "7-53";

        var actual = await sut.GetBackstoryAnswerById(answerId);

        Assert.Equal(answerId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_backstory_questions_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        HashSet<int> ids = new()
        {
            7,
            10,
            11
        };

        var actual = await sut.GetBackstoryQuestionsByIds(ids);

        Assert.Collection(actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids));
    }

    [Fact]
    public async Task It_can_get_backstory_answers_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        HashSet<string> ids = new()
        {
            "7-53",
            "7-54",
            "7-55"
        };

        var actual = await sut.GetBackstoryAnswersByIds(ids);

        Assert.Collection(actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids));
    }

    [Fact]
    public async Task It_can_get_backstory_questions_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryQuestionsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_backstory_answers_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<StoryJournal>();

        var actual = await sut.GetBackstoryAnswersByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
