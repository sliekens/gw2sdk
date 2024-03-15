using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class BuffJson
{
    public static Buff GetBuff(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember skillId = "skill_id";
        OptionalMember description = "description";
        foreach (var member in json.EnumerateObject())
        {
            if (skillId.Match(member))
            {
                skillId = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Buff
        {
            SkillId = skillId.Map(value => value.GetInt32()),
            Description = description.Map(value => value.GetString()) ?? ""
        };
    }
}
