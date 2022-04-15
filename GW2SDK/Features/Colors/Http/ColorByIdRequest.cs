using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http;

[PublicAPI]
public sealed class ColorByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public ColorByIdRequest(int colorId, Language? language)
    {
        ColorId = colorId;
        Language = language;
    }

    public int ColorId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ColorByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.ColorId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
