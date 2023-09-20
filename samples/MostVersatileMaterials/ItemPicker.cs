using GuildWars2.Items;
using Spectre.Console;

namespace MostVersatileMaterials;

public static class ItemPicker
{
    public static Item Prompt(IReadOnlyList<Item> ingredients) =>
        AnsiConsole.Prompt(
            new SelectionPrompt<Item>().Title("Pick an ingredient to see the available recipes")
                .MoreChoicesText("Scroll down for less commonly used ingredients")
                .AddChoices(ingredients)
                .UseConverter(item => item.Name)
                .PageSize(20)
        );
}
