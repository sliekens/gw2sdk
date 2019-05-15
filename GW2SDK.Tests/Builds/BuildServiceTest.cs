using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds;
using GW2SDK.Builds.Infrastructure;
using Xunit;

namespace GW2SDK.Tests.Builds
{
    public class BuildServiceTest
    {
        private BuildService CreateSut()
        {
            return new BuildService(new JsonBuildService(new HttpClient
            {
                BaseAddress = new Uri("https://api.guildwars2.com", UriKind.Absolute)
            }));
        }

        [Fact]
        public async Task GetBuild_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetBuild();

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task GetBuild_IdShouldBePositiveNumber()
        {
            var sut = CreateSut();

            var actual = await sut.GetBuild();

            Assert.InRange(actual.Id, 1, int.MaxValue);
        }
    }
}