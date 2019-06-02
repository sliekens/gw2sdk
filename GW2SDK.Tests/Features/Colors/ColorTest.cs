using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Colors.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    [Collection(nameof(ColorDbCollection))]
    public class ColorTest
    {
        public ColorTest(ColorFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ColorFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Color>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotEmpty(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Cloth_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotNull(actual.Cloth);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Leather_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotNull(actual.Leather);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Metal_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotNull(actual.Metal);
                });
        }

        [Fact(Skip = "Some dyes like Hydra (1594) don't have a 'fur' property. Bug in API?")]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Fur_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotNull(actual.Fur);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Categories_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.NotNull(actual.Categories);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void BaseRgb_ShouldBeRgbTuple()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            Assert.All(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.Collection(actual.BaseRgb,
                        red => Assert.InRange(red,     1, 255),
                        green => Assert.InRange(green, 1, 255),
                        blue => Assert.InRange(blue,   1, 255));
                });
        }
    }
}
