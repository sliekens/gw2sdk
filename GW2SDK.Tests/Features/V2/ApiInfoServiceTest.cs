using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.V2;
using Xunit;

namespace GW2SDK.Tests.Features.V2
{
    public class ApiInfoServiceTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_current_api_info()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ApiInfoService>();

            var actual = await sut.GetApiInfo();

            Assert.IsType<ApiInfo>(actual);
        }
    }
}
