using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds;
using GW2SDK.Builds.Infrastructure;
using Xunit;

namespace GW2SDK.Tests.Builds
{
    public class BuildServiceTest : IClassFixture<ConfigurationFixture>
    {
        private readonly ConfigurationFixture _fixture;

        public BuildServiceTest(ConfigurationFixture fixture)
        {
            _fixture = fixture;
        }

        private BuildService CreateSut()
        {
            return new BuildService(new JsonBuildService(new HttpClient
            {
                BaseAddress = _fixture.BaseAddress
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