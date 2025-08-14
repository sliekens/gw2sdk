using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Objectives;

internal static class SkillObjectiveJson
{
    public static SkillObjective GetSkillObjective(this in JsonElement json)
    {
        RequiredMember cost = "cost";
        RequiredMember skillId = "skill_id";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Skill"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (skillId.Match(member))
            {
                skillId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SkillObjective
        {
            Cost = cost.Map(static (in JsonElement value) => value.GetInt32()),
            SkillId = skillId.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
