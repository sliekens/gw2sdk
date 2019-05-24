using System.Collections.Generic;
using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Colors.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorTest : IClassFixture<ColorFixture>
    {
        public ColorTest(ColorFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        private readonly ColorFixture _fixture;

        private List<Color> CreateSut(JsonSerializerSettings jsonSerializerSettings)
        {
            var sut = new List<Color>();
            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, sut, jsonSerializerSettings);
            return sut;
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Color_ShouldHaveNoMissingMembers()
        {
            _ = CreateSut(new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Id_ShouldBePositive()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.InRange(color.Id, 1, int.MaxValue); });
        }


        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Name_ShouldNotBeEmpty()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotEmpty(color.Name); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Cloth_ShouldNotBeNull()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotNull(color.Cloth); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Leather_ShouldNotBeNull()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotNull(color.Leather); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Metal_ShouldNotBeNull()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotNull(color.Metal); });
        }

        [Fact(Skip = "Some dyes like Hydra (1594) don't have a 'fur' property. Bug in API?")]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Fur_ShouldNotBeNull()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotNull(color.Fur); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Categories_ShouldNotBeNull()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color => { Assert.NotNull(color.Categories); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_BaseRgb_ShouldBeRgbTuple()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut, color =>
            {
                Assert.Collection(color.BaseRgb,
                    red => Assert.InRange(red, 1, 255),
                    green => Assert.InRange(green, 1, 255),
                    blue => Assert.InRange(blue, 1, 255));
            });
        }
    }
}
