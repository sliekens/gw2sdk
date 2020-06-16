using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Backstories.Questions.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories.Questions.Fixtures
{
    public class BackstoryQuestionFixture : IAsyncLifetime
    {
        public List<string> BackstoryQuestions { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");
            BackstoryQuestions = await GetAllBackstoryQuestions(http);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllBackstoryQuestions(HttpClient http)
        {
            var request = new BackstoryQuestionsRequest();
            using var response = await http.SendAsync(request);
            using var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(responseReader);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            var array = await JToken.ReadFromAsync(jsonReader);
            return array.Children<JObject>().Select(obj => obj.ToString(Formatting.None)).ToList();
        }
    }
}
