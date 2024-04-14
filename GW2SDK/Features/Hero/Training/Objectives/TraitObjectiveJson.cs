using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Objectives;

internal static class TraitObjectiveJson
{
    public static TraitObjective GetTraitObjective(
        this JsonElement json
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
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (traitId.Match(member))
            {
                traitId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitObjective
        {
            Cost = cost.Map(static value => value.GetInt32()),
            TraitId = traitId.Map(static value => value.GetInt32())
        };
    }
}
