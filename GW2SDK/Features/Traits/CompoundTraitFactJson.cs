using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public static class CompoundTraitFactJson
{
    public static CompoundTraitFact GetCompoundTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var fact = json.GetTraitFact(
            missingMemberBehavior,
            out var requiresTrait,
            out var overrides
        );
        return new CompoundTraitFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }
}
