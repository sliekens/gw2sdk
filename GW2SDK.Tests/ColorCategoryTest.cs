using GW2SDK.Colors;
using GW2SDK.Colors.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GW2SDK.Tests
{
    public class ColorCategoryTest
    {
        private readonly HttpClient http;

        public ColorCategoryTest()
        {
            http = new HttpClient
            {
                BaseAddress = new Uri("https://api.guildwars2.com")
            };
        }

        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Feature", "Colors")]
        public async Task ColorCategory_ShouldIncludeAllKnownValues()
        {
            var service = new JsonColorService(http);
            var categories = await service.GetAllColorCategories();

            var missingCategories = categories.Except(Enum.GetNames(typeof(ColorCategory)));

            Assert.Empty(missingCategories);
        }
    }
}
