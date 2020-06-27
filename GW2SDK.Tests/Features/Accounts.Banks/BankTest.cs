using GW2SDK.Accounts.Banks;
using GW2SDK.Tests.Features.Accounts.Banks.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Banks
{
    public class BankTest : IClassFixture<BankFixture>
    {
        public BankTest(BankFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly BankFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        public void Bank_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            var actual = JsonConvert.DeserializeObject<Bank>(_fixture.Bank, settings);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(0, actual.Count % 30);
        }
    }
}
