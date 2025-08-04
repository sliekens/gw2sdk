using GuildWars2.Hero.Crafting.Daily;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Hero.Crafting.Recipes;

namespace GuildWars2.Hero.Crafting;

/// <summary>Provides query methods for APIs related to crafting. This class consists of logical groups containing related
/// sets of APIs. For example, all APIs pertaining to recipes are grouped into <see cref="Recipes" />.</summary>
public sealed class CraftingClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="CraftingClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public CraftingClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <inheritdoc cref="CraftingDisciplinesClient" />
    public CraftingDisciplinesClient Disciplines => new(httpClient);

    /// <inheritdoc cref="RecipesClient" />
    public RecipesClient Recipes => new(httpClient);

    /// <inheritdoc cref="DailyCraftingClient" />
    public DailyCraftingClient Daily => new(httpClient);
}
