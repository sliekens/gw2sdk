using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class SkillObjectiveJson
{
    public static SkillObjective GetSkillObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember cost = new("cost");
        RequiredMember skillId = new("skill_id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Skill"))
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
            else if (member.NameEquals(skillId.Name))
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
            Cost = cost.Select(value => value.GetInt32()),
            SkillId = skillId.Select(value => value.GetInt32())
        };
    }
}
