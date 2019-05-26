using System.Threading.Tasks;
using GW2SDK.Features.Accounts;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest : IClassFixture<HttpFixture>
    {
        public AccountServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_ShouldReturnAccount()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.IsType<Account>(actual);
        }
    }
}
