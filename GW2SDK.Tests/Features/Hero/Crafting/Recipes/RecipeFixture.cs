using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

// ReSharper disable once ClassNeverInstantiated.Global
public class RecipeFixture
{
    public IReadOnlyCollection<string> Recipes { get; } =
        FlatFileReader.Read("Data/recipes.json.gz").ToList().AsReadOnly();
}
