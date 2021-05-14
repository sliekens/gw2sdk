using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Backstories.Questions.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories.Questions.Fixtures
{
    public class BackstoryQuestionFixture : IAsyncLifetime
    {
        public List<string> BackstoryQuestions { get; set; }

        public async Task InitializeAsync()
        {
            await using var services = new Composer();
            var http = services.Resolve<HttpClient>();
            BackstoryQuestions = await GetAllBackstoryQuestions(http);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllBackstoryQuestions(HttpClient http)
        {
            var request = new BackstoryQuestionsRequest();
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}
