using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Subtokens.Fixtures;
using GW2SDK.Tests.Shared;
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
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = JsonConvert.DeserializeObject<CreatedSubtoken>(_fixture.CreatedSubtoken, settings);
        }
    }
}
