using System.Text.Json;
using GuildWars2.Hero.Training.Objectives;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingObjectiveJson
{
    public static TrainingObjective GetTrainingObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Skill":
                    return json.GetSkillObjective(missingMemberBehavior);
                case "Trait":
                    return json.GetTraitObjective(missingMemberBehavior);
            }
        }

        RequiredMember cost = "cost";
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
            else if (cost.Match(member))
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
