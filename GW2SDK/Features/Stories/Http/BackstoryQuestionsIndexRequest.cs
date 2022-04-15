using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryQuestionsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/questions")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(BackstoryQuestionsIndexRequest _) => Template.Compile();
}
