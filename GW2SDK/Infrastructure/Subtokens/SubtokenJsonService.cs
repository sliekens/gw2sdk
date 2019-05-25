using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Features.Subtokens;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class SubtokenJsonService : ISubtokenJsonService
    {
        private readonly HttpClient _http;

        public SubtokenJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        // This is a little weird but in order for this to work, you need to add a token to HttpClient.DefaultRequestHeaders
        public async Task<HttpResponseMessage> CreateSubtoken(
            IReadOnlyList<Permission> permissions = default,
            IReadOnlyList<Uri> urls = default,
            DateTimeOffset? expire = default)
        {
            // TODO: pass arguments!
            using (var request = new CreateSubtokenRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }
    }
}
