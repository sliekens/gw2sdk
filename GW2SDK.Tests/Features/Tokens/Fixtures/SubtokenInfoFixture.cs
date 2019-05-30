using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure.Tokens;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class SubtokenInfoFixture : IAsyncLifetime
    {
        public string SubtokenInfoJson { get; private set; }

        public IReadOnlyList<Permission> SubtokenPermissions { get; private set; }

        public DateTimeOffset CreatedSubtokenDate { get; private set; }

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();

            var subtokenService = new SubtokenService(http);

            SubtokenPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToList();

            var createdSubtoken = await subtokenService.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, SubtokenPermissions);

            using (var request = new GetTokenInfoRequest.Builder(createdSubtoken.Subtoken).GetRequest())
            using (var response = await http.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                CreatedSubtokenDate = response.Headers.Date.GetValueOrDefault(DateTimeOffset.Now);
                SubtokenInfoJson = await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
