using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Annotations;

namespace GW2SDK.Http
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class HttpClientExtensions
    {
        public static void UseAccessToken(this HttpClient instance, string accessToken)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            if (accessToken is null) throw new ArgumentNullException(nameof(accessToken));
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentException("Access token cannot be null or whitespace.", nameof(accessToken));
            instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static void UseLatestSchemaVersion(this HttpClient instance)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            instance.DefaultRequestHeaders.Add("X-Schema-Version", "latest");
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

        public static void UseDataCompression(this HttpClient instance)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate",  0.5));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("identity", 0));
        }
    }
}
