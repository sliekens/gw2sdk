using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Exceptions;
using GW2SDK.Http;
using GW2SDK.Impl;
using Xunit;

namespace GW2SDK.Tests.Http
{
    public class RateLimitHandlerTest
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task Handler_returns_response_when_under_rate_limit(HttpStatusCode statusCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(statusCode, @"{ ""success"": true }");

            var sut = new RateLimitHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await httpClient.SendAsync(request);

            Assert.NotNull(actual);
            Assert.Equal(stubHttpMessageHandler.Code,    actual.StatusCode);
            Assert.Equal(stubHttpMessageHandler.Content, await actual.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Handler_throws_when_over_rate_limit()
        {
            const string message = "too many requests";
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(HttpStatusCodeEx.TooManyRequests, @$"{{ ""text"": ""{message}""}}");

            var sut = new RateLimitHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await Record.ExceptionAsync(async () => await httpClient.SendAsync(request));

            var reason = Assert.IsType<TooManyRequestsException>(actual);

            Assert.Equal(message, reason.Message);
        }
    }
}
