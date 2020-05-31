using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Annotations;

namespace GW2SDK.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class HttpClientExtensions
    {
        public static void UseBaseAddress(this HttpClient instance, Uri baseAddress)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.BaseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        public static void UseAccessToken(this HttpClient instance, string accessToken)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentException("Access token cannot be null or whitespace.", nameof(accessToken));
            instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static void UseSchemaVersion(this HttpClient instance, string version)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (version == null) throw new ArgumentNullException(nameof(version));

            // Lock to make this a little more thread-safe, assuming nobody tries to mutate X-Schema-Version directly
            lock (instance)
            {
                // Don't care if Remove returns true or false
                _ = instance.DefaultRequestHeaders.Remove("X-Schema-Version");
                instance.DefaultRequestHeaders.Add("X-Schema-Version", version);
            }
        }

        public static void UseLatestSchemaVersion(this HttpClient instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.UseSchemaVersion("latest");
        }

        public static void UseSchemaVersion(this HttpClient instance, DateTimeOffset version)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (version == null) throw new ArgumentNullException(nameof(version));

            // Use the sortable format (which is ISO 8601)
            // eg. 2019-05-16T12:43:57
            const string sortable = "s";

            // Make sure to remove the offset and append "Z" to indicate that it is UTC
            // eg. 2019-05-16T10:43:57Z
            // (There is no format string that does all this in, I checked)
            instance.UseSchemaVersion(version.ToOffset(TimeSpan.Zero).ToString(sortable) + "Z");
        }

        public static void UseLanguage(this HttpClient instance, string lang)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (lang == null) throw new ArgumentNullException(nameof(lang));

            instance.DefaultRequestHeaders.AcceptLanguage.ParseAdd(lang);
        }

        public static void UseDataCompression(this HttpClient instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate",  0.5));
            instance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("identity", 0));
        }
    }
}
