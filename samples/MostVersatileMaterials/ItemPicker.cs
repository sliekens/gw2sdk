using GuildWars2.Items;

using Spectre.Console;

namespace MostVersatileMaterials;

public static class ItemPicker
{
    public static Item Prompt(IReadOnlyList<(Item item, int count)> ingredients)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<(Item, int)>()
                    .Title("Pick an ingredient to see the available recipes")
                    .MoreChoicesText("Scroll down for less commonly used ingredients")
                    .AddChoices(ingredients)
                    .UseConverter(item => $"{item.Item1.Name} ({item.Item2} recipes)")
                    .PageSize(20)
            )
            .Item1;
    }
}
