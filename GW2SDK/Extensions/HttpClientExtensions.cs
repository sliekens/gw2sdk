using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Features.Common.Infrastructure;
using GW2SDK.Infrastructure;

namespace GW2SDK.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class HttpClientExtensions
    {
        
        public static async Task<(string Json, Dictionary<string, string> MetaData)> GetStringWithMetaDataAsync([NotNull] this HttpClient instance, Uri requestUri)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (var response = await instance.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var metaData = response.Headers.GetMetaData();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return (json, metaData);
            }
        }

        public static void UseBaseAddress([NotNull] this HttpClient instance, [NotNull] Uri baseAddress)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.BaseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        public static HttpClient WithBaseAddress([NotNull] this HttpClient instance, [NotNull] Uri baseAddress)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (baseAddress == null) throw new ArgumentNullException(nameof(baseAddress));
            instance.UseBaseAddress(baseAddress);
            return instance;
        }

        public static void UseAccessToken([NotNull] this HttpClient instance, [NotNull] string accessToken)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static HttpClient WithAccessToken([NotNull] this HttpClient instance, [NotNull] string accessToken)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            instance.UseAccessToken(accessToken);
            return instance;
        }

        public static void UseSchemaVersion([NotNull] this HttpClient instance, [NotNull] string version)
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

        public static HttpClient WithSchemaVersion([NotNull] this HttpClient instance, [NotNull] string version)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (version == null) throw new ArgumentNullException(nameof(version));
            instance.UseSchemaVersion(version);
            return instance;
        }

        public static void UseLatestSchemaVersion([NotNull] this HttpClient instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.UseSchemaVersion("latest");
        }

        public static HttpClient WithLatestSchemaVersion([NotNull] this HttpClient instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            instance.UseLatestSchemaVersion();
            return instance;
        }

        public static void UseSchemaVersion([NotNull] this HttpClient instance, DateTimeOffset version)
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

        public static HttpClient WithSchemaVersion([NotNull] this HttpClient instance, DateTimeOffset version)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (version == null) throw new ArgumentNullException(nameof(version));
            instance.UseSchemaVersion(version);
            return instance;
        }
    }
}
