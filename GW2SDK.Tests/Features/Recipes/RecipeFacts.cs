using GW2SDK.Recipes;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    internal static class RecipeFacts
    {
        public static void Id_is_positive(Recipe actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Output_item_id_is_positive(Recipe actual) =>
            Assert.InRange(actual.OutputItemId, 1, int.MaxValue);

        public static void Output_item_count_is_positive(Recipe actual) =>
            Assert.InRange(actual.OutputItemCount, 1, int.MaxValue);

        public static void Min_rating_is_between_0_and_500(Recipe actual) => Assert.InRange(actual.MinRating, 0, 500);

        public static void Time_to_craft_is_positive(Recipe actual) =>
            Assert.InRange(actual.TimeToCraft.Ticks, 1, long.MaxValue);

        public static void Validate(Recipe actual)
        {
            Id_is_positive(actual);
            Output_item_id_is_positive(actual);
            Output_item_count_is_positive(actual);
            Min_rating_is_between_0_and_500(actual);
            Time_to_craft_is_positive(actual);
        }
    }
}
