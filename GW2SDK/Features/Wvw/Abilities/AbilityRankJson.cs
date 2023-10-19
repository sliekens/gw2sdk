using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
public static class AbilityRankJson
{
    public static AbilityRank GetAbilityRank(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember cost = new("cost");
        RequiredMember effect = new("effect");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (member.NameEquals(effect.Name))
            {
                effect.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AbilityRank
        {
            Cost = cost.Select(value => value.GetInt32()),
            Effect = effect.Select(value => value.GetStringRequired())
        };
    }
}
