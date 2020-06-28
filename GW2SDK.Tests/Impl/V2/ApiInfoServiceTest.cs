using System.Threading.Tasks;
using GW2SDK.Impl.V2;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Impl.V2
{
    public class ApiInfoServiceTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_current_api_info()
        {
            await using var services = new Container();
            var sut = services.Resolve<ApiInfoService>();

            var actual = await sut.GetApiInfo();

            Assert.IsType<ApiInfo>(actual);
        }
    }
}
