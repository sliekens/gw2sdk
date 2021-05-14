using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http
{
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
            if (accessToken is null) throw new ArgumentNullException(nameof(accessToken));
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Access token cannot be null or whitespace.", nameof(accessToken));
            instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static void UseSchemaVersion(this HttpClient instance, SchemaVersion version)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            instance.DefaultRequestHeaders.Add("X-Schema-Version", version.Version);
        }

        public static void UseLanguage(this HttpClient instance, string lang)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            if (lang is null) throw new ArgumentNullException(nameof(lang));

            instance.DefaultRequestHeaders.AcceptLanguage.ParseAdd(lang);
        }

        public static async Task<JsonDocument> ReadAsJsonAsync(this HttpContent instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            await using var content = await instance.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonDocument.ParseAsync(content).ConfigureAwait(false);
        }
    }
}
