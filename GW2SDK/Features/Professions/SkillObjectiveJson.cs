using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public static class SkillObjectiveJson
{
    public static SkillObjective GetSkillObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> cost = new("cost");
        RequiredMember<int> skillId = new("skill_id");
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
                cost.Value = member.Value;
            }
            else if (member.NameEquals(skillId.Name))
            {
                skillId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillObjective
        {
            Cost = cost.GetValue(),
            SkillId = skillId.GetValue()
        };
    }
}
