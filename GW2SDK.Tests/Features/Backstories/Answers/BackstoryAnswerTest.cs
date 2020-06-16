using GW2SDK.Backstories.Answers;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Backstories.Answers.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Backstories.Answers
{
    public class BackstoryAnswerTest : IClassFixture<BackstoryAnswerFixture>
    {
        public BackstoryAnswerTest(ITestOutputHelper output, BackstoryAnswerFixture fixture)
        {
            _output = output;
            _fixture = fixture;
        }

        private readonly BackstoryAnswerFixture _fixture;
        private readonly ITestOutputHelper _output;

        private static class BackstoryAnswerFact
        {
            public static void Id_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Id);

            public static void Title_is_not_null(BackstoryAnswer actual) => Assert.NotNull(actual.Title);

            public static void Description_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Description);

            public static void Journal_is_not_empty(BackstoryAnswer actual) => Assert.NotEmpty(actual.Journal);

            public static void Has_a_question(BackstoryAnswer actual) => Assert.InRange(actual.Question, 1, 999);
        }

        [Fact]
        public void Backstory_questions_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.BackstoryAnswers,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<BackstoryAnswer>(json, settings);
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
