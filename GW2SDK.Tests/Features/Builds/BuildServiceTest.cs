using System.Threading.Tasks;
using GW2SDK.Builds;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest
    {
        [Fact]
        [Trait("Feature",  "Builds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_current_build()
        {
            await using var services = new Container();
            var sut = services.Resolve<BuildService>();

            var actual = await sut.GetBuild();

            Assert.IsType<Build>(actual);
        }
    }
}
