using System.Threading.Tasks;
using GW2SDK.Features.Builds;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest
    {
        [Fact]
        [Trait("Feature",  "Builds")]
        [Trait("Category", "Integration")]
        public async Task GetBuild_ShouldReturnBuild()
        {
            var services = new Container();
            var sut = services.Resolve<BuildService>();

            var actual = await sut.GetBuild();

            Assert.IsType<Build>(actual);
        }
    }
}
