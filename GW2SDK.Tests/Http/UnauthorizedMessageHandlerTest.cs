using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using Xunit;

namespace GW2SDK.Tests.Http
{
    public class UnauthorizedMessageHandlerTest
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Handler_returns_response_when_request_is_authorized(HttpStatusCode statusCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(statusCode, @"{ ""success"": true }");

            var sut = new UnauthorizedMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Assert.NotNull(actual);
            Assert.Equal(stubHttpMessageHandler.Code, actual.StatusCode);
            Assert.Equal(stubHttpMessageHandler.Content, await actual.Content.ReadAsStringAsync());
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, "Invalid access token")]
        [InlineData(HttpStatusCode.Forbidden, "requires scope")]
        public async Task Handlers_throws_when_request_is_unauthorized(HttpStatusCode statusCode, string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(statusCode, @$"{{ ""text"": ""{message}""}}");

            var sut = new UnauthorizedMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await Record.ExceptionAsync(async () => await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead));

            var reason = Assert.IsType<UnauthorizedOperationException>(actual);

            Assert.Equal(message, reason.Message);
        }
    }
}
