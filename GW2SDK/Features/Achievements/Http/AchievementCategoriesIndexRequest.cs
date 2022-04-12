using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AchievementCategoriesIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/categories")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(AchievementCategoriesIndexRequest _)
    {
        return Template.Compile();
    }
}