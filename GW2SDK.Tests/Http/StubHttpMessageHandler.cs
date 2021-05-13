using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Tests.Http
{
    internal class StubHttpMessageHandler : HttpMessageHandler
    {
        internal StubHttpMessageHandler(HttpStatusCode code, string content)
        {
            Code = code;
            Content = content;
        }

        internal HttpStatusCode Code { get; }

        internal string Content { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(new HttpResponseMessage(Code) { Content = new StringContent(Content, Encoding.UTF8, "application/json") });
    }
}
