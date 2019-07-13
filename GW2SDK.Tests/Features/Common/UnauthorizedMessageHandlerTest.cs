using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Exceptions;
using GW2SDK.Impl.HttpMessageHandlers;
using Newtonsoft.Json;
using Xunit;

namespace GW2SDK.Tests.Features.Common
{
    public class UnauthorizedMessageHandlerTest
    {
        public class StubHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _body;
            private readonly HttpStatusCode _code;

            public StubHttpMessageHandler(HttpStatusCode code, string body)
            {
                _code = code;
                _body = body;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
                Task.FromResult(new HttpResponseMessage(_code) { Content = new StringContent(_body) });
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public async Task SendAsync_WhenResponseCodeIs401_ShouldThrowUnauthorizedOperationException()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var responseStatus = HttpStatusCode.Unauthorized;
            var responseBody = JsonConvert.SerializeObject(new { text = "Invalid access token" });
            var stubHttpMessageHandler = new StubHttpMessageHandler(responseStatus, responseBody);

            var sut = new UnauthorizedMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var exception = await Record.ExceptionAsync(async () => await httpClient.SendAsync(request));

            var reason = Assert.IsType<UnauthorizedOperationException>(exception);

            Assert.Equal("Invalid access token", reason.Message);
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(HttpStatusCode.OK, "{}")]
        [InlineData(HttpStatusCode.BadRequest, "{}")]
        [InlineData(HttpStatusCode.Forbidden, "{}")]
        [InlineData(HttpStatusCode.ServiceUnavailable, "{}")]
        public async Task SendAsync_WhenResponseCodeIsNot401_ShouldReturnResponse(HttpStatusCode responseStatus, string responseBody)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(responseStatus, responseBody);

            var sut = new UnauthorizedMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var response = await httpClient.SendAsync(request);

            Assert.NotNull(response);
        }
    }
}
