using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Subtokens
{
    public interface ISubtokenJsonService
    {
        Task<HttpResponseMessage> CreateSubtoken(
            [CanBeNull] IReadOnlyList<Permission> permissions = default,
            [CanBeNull] [ItemNotNull] IReadOnlyList<Uri> urls = default,
            [CanBeNull] DateTimeOffset? expire = default);
    }
}
