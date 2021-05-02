using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using Xunit;

namespace GW2SDK.Tests.Http
{
    public class BadMessageHandlerTest
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCodeEx.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task Handler_returns_response_when_arguments_are_valid(HttpStatusCode statusCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(statusCode, @"{ ""success"": true }");

            var sut = new BadMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Assert.NotNull(actual);
            Assert.Equal(stubHttpMessageHandler.Code,    actual.StatusCode);
            Assert.Equal(stubHttpMessageHandler.Content, await actual.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Handler_throws_when_arguments_are_invalid()
        {
            const string message = "bad input";
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.guildwars2.com/v2.json");
            var stubHttpMessageHandler = new StubHttpMessageHandler(HttpStatusCode.BadRequest, @$"{{ ""text"": ""{message}""}}");

            var sut = new BadMessageHandler(stubHttpMessageHandler);

            var httpClient = new HttpClient(sut);

            var actual = await Record.ExceptionAsync(async () => await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead));

            var reason = Assert.IsType<ArgumentException>(actual);

            Assert.Equal(message, reason.Message);
        }
    }
}
