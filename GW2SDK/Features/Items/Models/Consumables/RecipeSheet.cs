namespace GuildWars2.Items;

[PublicAPI]
public sealed record RecipeSheet : Unlocker
{
    public required int RecipeId { get; init; }

    public required IReadOnlyCollection<int>? ExtraRecipeIds { get; init; }
}
