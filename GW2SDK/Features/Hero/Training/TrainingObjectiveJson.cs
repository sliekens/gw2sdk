using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingObjectiveJson
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

        RequiredMember cost = "cost";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == cost.Name)
            {
                cost = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingObjective { Cost = cost.Map(value => value.GetInt32()) };
    }
}
