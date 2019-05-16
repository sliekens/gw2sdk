using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Infrastructure;

namespace GW2SDK.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class HttpClientExtensions
    {
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
    }
}
