using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class TraitedSkillFactJson
{
    public static TraitedSkillFact GetTraitedSkillFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var fact = json.GetSkillFact(
            missingMemberBehavior,
            out var requiresTrait,
            out var overrides
        );
        return new TraitedSkillFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }
}
