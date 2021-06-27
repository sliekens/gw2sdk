using System.Threading.Tasks;
using GW2SDK.Builds;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest
    {
        private static class BuildFact
        {
            public static void Id_is_positive(Build actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",  "Builds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_current_build()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BuildService>();

            var actual = await sut.GetBuild();

            BuildFact.Id_is_positive(actual);
        }
    }
}
