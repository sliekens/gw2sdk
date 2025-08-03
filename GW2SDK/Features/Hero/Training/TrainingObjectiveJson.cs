using System.Text.Json;

using GuildWars2.Hero.Training.Objectives;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingObjectiveJson
{
    public static TrainingObjective GetTrainingObjective(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Skill":
                    return json.GetSkillObjective();
                case "Trait":
                    return json.GetTraitObjective();
                default:
                    break;
            }
        }

        RequiredMember cost = "cost";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
                }
            }
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new TrainingObjective { Cost = cost.Map(static (in JsonElement value) => value.GetInt32()) };
    }
}
