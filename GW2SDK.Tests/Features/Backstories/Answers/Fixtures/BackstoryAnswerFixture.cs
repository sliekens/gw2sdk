using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Backstories.Answers.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories.Answers.Fixtures
{
    public class BackstoryAnswerFixture : IAsyncLifetime
    {
        public List<string> BackstoryAnswers { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");
            BackstoryAnswers = await GetAllBackstoryAnswers(http);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllBackstoryAnswers(HttpClient http)
        {
            var request = new BackstoryAnswersRequest();
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
