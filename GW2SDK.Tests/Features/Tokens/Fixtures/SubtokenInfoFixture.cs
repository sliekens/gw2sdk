using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Subtokens;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens.Http;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class SubtokenInfoFixture : IAsyncLifetime
    {
        public string SubtokenInfoJson { get; private set; }

        public IReadOnlyCollection<Permission> SubtokenPermissions { get; private set; }

        public DateTimeOffset CreatedSubtokenDate { get; private set; }

        public DateTimeOffset ExpiresAt { get; private set; }

        public IReadOnlyCollection<string> Urls { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<HttpClient>();

            var subtokenService = container.Resolve<SubtokenService>();

            SubtokenPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToList();

            var exp = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(exp.ToUnixTimeSeconds());

            Urls = new List<string> { Location.Tokeninfo, Location.Account, Location.Characters + "/My Cool Character" };

            var createdSubtoken = await subtokenService.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, SubtokenPermissions, ExpiresAt, Urls);

            var request = new TokenInfoRequest(createdSubtoken.Subtoken);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            CreatedSubtokenDate = response.Headers.Date.GetValueOrDefault(DateTimeOffset.Now);
            SubtokenInfoJson = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
