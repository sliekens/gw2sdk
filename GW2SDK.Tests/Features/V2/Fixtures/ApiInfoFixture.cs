using System.IO;

namespace GW2SDK.Tests.Features.V2.Fixtures
{
    public class ApiInfoFixture
    {
        public ApiInfoFixture()
        {
            ApiInfo = File.ReadAllText("Data/v2.json");
        }

        public string ApiInfo { get; }
    }
}
