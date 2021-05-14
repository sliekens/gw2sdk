using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Subtokens;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens.Http;
using Polly;
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
            await using var services = new Composer();
            var http = services.Resolve<HttpClient>();

            var subtokenService = services.Resolve<SubtokenService>();

            SubtokenPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToList();

            var exp = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(exp.ToUnixTimeSeconds());

            Urls = new List<string>
            {
                Location.Tokeninfo,
                Location.Account,
                Location.Characters + "/My Cool Character"
            };

            var createdSubtoken = await subtokenService.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull,
                SubtokenPermissions,
                ExpiresAt,
                Urls);

            // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
            // I guess this is a timing problem, because a retry usually does pass.
            // Maybe they are they checking the "iat" claim and treating it as "nbf", without considering clock skew? One can only guess.
            // Just to be sure that this error is real, retry up to 10 times with 200ms delays before throwing an exception.
            //    Because 2 seconds should be enough to compensate for clock skew, without adding too much delay to true error results.
            await Policy.Handle<UnauthorizedOperationException>()
                .WaitAndRetryAsync(10, attempt => TimeSpan.FromMilliseconds(200))
                .ExecuteAsync(async () =>
                {
                    var request = new TokenInfoRequest(createdSubtoken.Subtoken);
                    using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    CreatedSubtokenDate = response.Headers.Date.GetValueOrDefault(DateTimeOffset.Now);
                    SubtokenInfoJson = await response.Content.ReadAsStringAsync();
                });
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
