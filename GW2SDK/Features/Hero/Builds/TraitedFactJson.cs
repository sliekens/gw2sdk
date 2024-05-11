using System.Text.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitedFactJson
{
    public static TraitedFact GetTraitedFact(this JsonElement json)
    {
        var fact = json.GetFact(out var requiresTrait, out var overrides);
        return new TraitedFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }
}
