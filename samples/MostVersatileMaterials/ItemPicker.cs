using GuildWars2.Items;

using Spectre.Console;

namespace MostVersatileMaterials;

internal static class ItemPicker
{
    private static string s_lastSearch = "";

    public static async Task<Item> PromptAsync(
        IReadOnlyList<(Item item, int count)> ingredients,
        CancellationToken cancellationToken = default)
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("How would you like to find an ingredient?")
                .AddChoices("Browse all ingredients", "Search by name")
        );

        if (choice == "Search by name")
        {
            return await SearchForItemAsync(ingredients, cancellationToken).ConfigureAwait(false);
        }

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

    private static async Task<Item> SearchForItemAsync(
        IReadOnlyList<(Item item, int count)> ingredients,
        CancellationToken cancellationToken)
    {
        while (true)
        {
            TextPrompt<string> searchPrompt = new TextPrompt<string>("Enter item name to search:")
                .AllowEmpty();

            // Use editable default value from Spectre.Console 0.55+
            // This pre-populates the prompt with the last search term, allowing users to edit it
            if (!string.IsNullOrEmpty(s_lastSearch))
            {
                searchPrompt.DefaultValue(s_lastSearch).ShowDefaultValue(true);
            }

            // Use CancellationToken support from Spectre.Console 0.55+
            // This allows programmatic cancellation of the prompt (e.g., via Ctrl+C)
            string searchTerm = await searchPrompt
                .ShowAsync(AnsiConsole.Console, cancellationToken)
                .ConfigureAwait(false);

            s_lastSearch = searchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                AnsiConsole.MarkupLine("[red]Search term cannot be empty. Please try again.[/]");
                continue;
            }

            List<(Item item, int count)> matches = ingredients
                .Where(tuple => tuple.item.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
            {
                AnsiConsole.MarkupLine($"[yellow]No items found matching '{searchTerm}'. Please try again.[/]");
                continue;
            }

            if (matches.Count == 1)
            {
                AnsiConsole.MarkupLine($"[green]Found: {matches[0].item.Name}[/]");
                return matches[0].item;
            }

            return AnsiConsole.Prompt(
                    new SelectionPrompt<(Item, int)>()
                        .Title($"Found {matches.Count} items matching '{searchTerm}'")
                        .AddChoices(matches)
                        .UseConverter(item => $"{item.Item1.Name} ({item.Item2} recipes)")
                        .PageSize(20)
                )
                .Item1;
        }
    }
}
