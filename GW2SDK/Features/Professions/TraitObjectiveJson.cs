using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class TraitObjectiveJson
{
    public static TraitObjective GetTraitObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember cost = "cost";
        RequiredMember traitId = "trait_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Trait"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = member;
            }
            else if (member.NameEquals(traitId.Name))
            {
                traitId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitObjective
        {
            Cost = cost.Map(value => value.GetInt32()),
            TraitId = traitId.Map(value => value.GetInt32())
        };
    }
}
