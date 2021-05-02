using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Backstories.Answers.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories.Answers.Fixtures
{
    public class BackstoryAnswerFixture : IAsyncLifetime
    {
        public List<string> BackstoryAnswers { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<HttpClient>();
            BackstoryAnswers = await GetAllBackstoryAnswers(http);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllBackstoryAnswers(HttpClient http)
        {
            var request = new BackstoryAnswersRequest();
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync();
            return json.Indent(false).RootElement.EnumerateArray().Select(item => item.ToString()).ToList();
        }
    }
}
