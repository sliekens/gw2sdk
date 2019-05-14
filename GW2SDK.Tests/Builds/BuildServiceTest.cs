using Xunit;

namespace GW2SDK.Tests.Builds
{
    public class BuildServiceTest : IClassFixture<BuildServiceFixture>
    {
        public BuildServiceTest(BuildServiceFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly BuildServiceFixture _fixture;

        [Fact]
        public void Build_ShouldNotReturnNull()
        {
            Assert.NotNull(_fixture.Build);
        }

        [Fact]
        public void BuildId_ShouldBePositiveNumber()
        {
            Assert.InRange(_fixture.Build.Id, 1, int.MaxValue);
        }
    }
}