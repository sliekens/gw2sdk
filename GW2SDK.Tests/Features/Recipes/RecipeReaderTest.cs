using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Recipes;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class RecipeReaderTest : IClassFixture<RecipeFixture>
    {
        public RecipeReaderTest(RecipeFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly RecipeFixture _fixture;

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
            var sut = new RecipeReader();

            AssertEx.ForEach(_fixture.Recipes,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    RecipeFact.Id_is_positive(actual);
                    RecipeFact.Output_item_id_is_positive(actual);
                    RecipeFact.Output_item_count_is_positive(actual);
                    RecipeFact.Min_rating_is_between_0_and_500(actual);
                    RecipeFact.Time_to_craft_is_positive(actual);
                });
        }
    }
}
