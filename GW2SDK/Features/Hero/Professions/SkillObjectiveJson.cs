using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Professions;

internal static class SkillObjectiveJson
{
    public static SkillObjective GetSkillObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember cost = "cost";
        RequiredMember skillId = "skill_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Skill"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == cost.Name)
            {
                cost = member;
            }
            else if (member.Name == skillId.Name)
            {
                skillId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillObjective
        {
            Cost = cost.Map(value => value.GetInt32()),
            SkillId = skillId.Map(value => value.GetInt32())
        };
    }
}
