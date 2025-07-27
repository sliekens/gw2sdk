using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

internal static class AbilityRankJson
{
    public static AbilityRank GetAbilityRank(this in JsonElement json)
    {
        RequiredMember cost = "cost";
        RequiredMember effect = "effect";

        foreach (var member in json.EnumerateObject())
        {
            if (cost.Match(member))
            {
                cost = member;
            }
            else if (effect.Match(member))
            {
                effect = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AbilityRank
        {
            Cost = cost.Map(static (in JsonElement value) => value.GetInt32()),
            Effect = effect.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
