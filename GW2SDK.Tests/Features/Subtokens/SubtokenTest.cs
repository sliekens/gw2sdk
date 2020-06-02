using GW2SDK.Impl.JsonConverters;
using GW2SDK.Subtokens;
using GW2SDK.Tests.Features.Subtokens.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenTest : IClassFixture<SubtokenFixture>
    {
        public SubtokenTest(SubtokenFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly SubtokenFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Subtokens")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Subtokens_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            var actual = JsonConvert.DeserializeObject<CreatedSubtoken>(_fixture.CreatedSubtoken, settings);

            Assert.NotEmpty(actual.Subtoken);
        }
    }
}
