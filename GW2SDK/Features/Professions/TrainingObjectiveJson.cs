using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class TrainingObjectiveJson
{
    public static TrainingObjective GetTrainingObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Skill":
                return json.GetSkillObjective(missingMemberBehavior);
            case "Trait":
                return json.GetTraitObjective(missingMemberBehavior);
        }

        RequiredMember cost = new("cost");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingObjective { Cost = cost.Select(value => value.GetInt32()) };
    }
}
