using System.Net;
using System.Text;

namespace GuildWars2.Tests.Http;

internal class StubHttpMessageHandler : HttpMessageHandler
{
    internal StubHttpMessageHandler(HttpStatusCode code, string content)
    {
        Code = code;
        Content = content;
    }

    internal HttpStatusCode Code { get; }

    internal string Content { get; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) =>
        Task.FromResult(
            new HttpResponseMessage(Code)
            {
                Content = new StringContent(Content, Encoding.UTF8, "application/json")
            }
        );
}
