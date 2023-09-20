using GuildWars2.Crafting;

namespace GuildWars2.Tests.Features.Crafting;

internal static class Invariants
{
    internal static void Has_id(this Recipe actual) => Assert.True(actual.Id > 0);

    internal static void Has_output_item_id(this Recipe actual) =>
        Assert.InRange(actual.OutputItemId, 1, int.MaxValue);

    internal static void Has_item_count(this Recipe actual) =>
        Assert.InRange(actual.OutputItemCount, 1, int.MaxValue);

    internal static void Has_min_rating_between_0_and_500(this Recipe actual) =>
        Assert.InRange(actual.MinRating, 0, 500);

    internal static void Has_time_to_craft(this Recipe actual) =>
        Assert.InRange(actual.TimeToCraft.Ticks, 1, long.MaxValue);
}
