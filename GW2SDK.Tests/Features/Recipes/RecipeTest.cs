using GW2SDK.Impl.JsonConverters;
using GW2SDK.Recipes;
using GW2SDK.Tests.Features.Recipes.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Recipes
{
    [Collection(nameof(RecipeDbCollection))]
    public class RecipeTest
    {
        public RecipeTest(RecipeFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly RecipeFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class RecipeFact
        {
            public static void Id_is_positive(Recipe actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Output_item_id_is_positive(Recipe actual) => Assert.InRange(actual.OutputItemId, 1, int.MaxValue);

            public static void Output_item_count_is_positive(Recipe actual) => Assert.InRange(actual.OutputItemCount, 1, int.MaxValue);

            public static void Min_rating_is_between_0_and_500(Recipe actual) => Assert.InRange(actual.MinRating, 0, 500);

            public static void Time_to_craft_is_positive(Recipe actual) => Assert.InRange(actual.TimeToCraft.Ticks, 1, long.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Recipes")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Recipes_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);

                    RecipeFact.Id_is_positive(actual);
                    RecipeFact.Output_item_id_is_positive(actual);
                    RecipeFact.Output_item_count_is_positive(actual);
                    RecipeFact.Min_rating_is_between_0_and_500(actual);
                    RecipeFact.Time_to_craft_is_positive(actual);
                });
        }
    }
}
