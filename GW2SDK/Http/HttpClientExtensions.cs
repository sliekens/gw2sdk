using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Http;

[PublicAPI]
public static class HttpClientExtensions
{
    /// <summary>Sets the <see cref="HttpClient.BaseAddress" /> to <c>"https://api.guildwars2.com"</c>.</summary>
    public static void UseGuildWars2(this HttpClient instance)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        instance.BaseAddress = new Uri("https://api.guildwars2.com", UriKind.Absolute);
    }

    public static void UseAccessToken(this HttpClient instance, string accessToken)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        Check.String(accessToken, nameof(accessToken));

        instance.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public static void UseLanguage(this HttpClient instance, Language language)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        if (language is null) throw new ArgumentNullException(nameof(language));

        instance.DefaultRequestHeaders.AcceptLanguage.ParseAdd(language.Alpha2Code);
    }

    internal static HttpClient? WithDefaults(this HttpClient? instance)
    {
        if (instance is null) return null;
        if (instance.BaseAddress is null) UseGuildWars2(instance);
        return instance;
    }
}
