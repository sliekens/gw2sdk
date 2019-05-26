using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest : IClassFixture<HttpFixture>
    {
        public ColorServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorIds_ShouldNotReturnEmptyCollection()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorIds(settings);

            Assert.NotEmpty(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldNotReturnNull()
        {
            var sut = new ColorService(_http.Http);

            const int dyeRemoverId = 1;
            
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorById(dyeRemoverId, settings);

            Assert.NotNull(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsById_ShouldReturnExpectedRange()
        {
            var sut = new ColorService(_http.Http);

            var ids = Enumerable.Range(1, 5).ToList();
            
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorsById(ids, settings);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsPage_ShouldReturnExpectedLimit()
        {
            var sut = new ColorService(_http.Http);
            
            var limit = 50;
            
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorsPage(0, limit, settings);

            Assert.InRange(actual.Count, 0, limit);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_ShouldHaveNoMissingMembers()
        {
            var sut = new ColorService(_http.Http);
            
            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            // Next statement throws if there are missing members
            _ = await sut.GetAllColors(settings);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Id_ShouldBePositive()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.InRange(color.Id, 1, int.MaxValue); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Name_ShouldNotBeEmpty()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotEmpty(color.Name); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Cloth_ShouldNotBeNull()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotNull(color.Cloth); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Leather_ShouldNotBeNull()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotNull(color.Leather); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Metal_ShouldNotBeNull()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotNull(color.Metal); });
        }

        [Fact(Skip = "Some dyes like Hydra (1594) don't have a 'fur' property. Bug in API?")]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Fur_ShouldNotBeNull()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotNull(color.Fur); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_Categories_ShouldNotBeNull()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color => { Assert.NotNull(color.Categories); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task Color_BaseRgb_ShouldBeRgbTuple()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetAllColors(settings);

            Assert.All(actual, color =>
            {
                Assert.Collection(color.BaseRgb,
                    red => Assert.InRange(red, 1, 255),
                    green => Assert.InRange(green, 1, 255),
                    blue => Assert.InRange(blue, 1, 255));
            });
        }
    }
}
