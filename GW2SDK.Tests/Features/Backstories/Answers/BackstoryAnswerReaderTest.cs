using System.Text.Json;
using GW2SDK.Backstories;
using GW2SDK.Backstories.Answers;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Backstories.Answers.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories.Answers
{
    public class BackstoryAnswerReaderTest : IClassFixture<BackstoryAnswerFixture>
    {
        public BackstoryAnswerReaderTest(BackstoryAnswerFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly BackstoryAnswerFixture _fixture;

        private static class BackstoryAnswerFact
        {
            public static void Id_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Id);

            public static void Title_is_not_null(BackstoryAnswer actual) => Assert.NotNull(actual.Title);

            public static void Description_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Description);

            public static void Journal_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Journal);

            public static void Has_a_question(BackstoryAnswer actual) => Assert.InRange(actual.Question, 1, 999);
        }

        [Fact]
        public void Backstory_answers_can_be_created_from_json()
        {
            var sut = new BackstoryReader();

            AssertEx.ForEach(_fixture.BackstoryAnswers,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Answer.Read(document.RootElement, MissingMemberBehavior.Error);

                    Assert.NotNull(actual);
                    BackstoryAnswerFact.Id_is_not_empty(actual);
                    BackstoryAnswerFact.Title_is_not_null(actual);
                    BackstoryAnswerFact.Description_is_not_empty(actual);
                    BackstoryAnswerFact.Journal_is_not_empty(actual);
                    BackstoryAnswerFact.Has_a_question(actual);
                });
        }
    }
}
