using GW2SDK.Backstories.Questions;
using GW2SDK.Tests.Features.Backstories.Questions.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Backstories.Questions
{
    public class BackstoryQuestionTest : IClassFixture<BackstoryQuestionFixture>
    {
        public BackstoryQuestionTest(ITestOutputHelper output, BackstoryQuestionFixture fixture)
        {
            _output = output;
            _fixture = fixture;
        }

        private readonly ITestOutputHelper _output;

        private readonly BackstoryQuestionFixture _fixture;

        private static class BackstoryQuestionFact
        {
            public static void Id_is_positive(BackstoryQuestion actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Title_is_not_null(BackstoryQuestion actual) => Assert.NotNull(actual.Title);

            public static void Description_is_not_empty(BackstoryQuestion actual) => Assert.NotEmpty(actual.Description);

            public static void Has_3_to_8_answers(BackstoryQuestion actual) => Assert.InRange(actual.Answers.Length, 3, 8);
        }

        [Fact]
        public void Backstory_questions_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.BackstoryQuestions,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<BackstoryQuestion>(json, settings);
                    Assert.NotNull(actual);
                    BackstoryQuestionFact.Id_is_positive(actual);
                    BackstoryQuestionFact.Title_is_not_null(actual);
                    BackstoryQuestionFact.Description_is_not_empty(actual);
                    BackstoryQuestionFact.Has_3_to_8_answers(actual);
                });
        }
    }
}
