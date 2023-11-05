using System.Text.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitedFactJson
{
    public static TraitedFact GetTraitedFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var fact = json.GetFact(missingMemberBehavior, out var requiresTrait, out var overrides);
        return new TraitedFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }
}
