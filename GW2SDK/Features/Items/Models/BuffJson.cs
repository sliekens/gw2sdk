using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class BuffJson
{
    public static Buff GetBuff(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember skillId = new("skill_id");
        OptionalMember description = new("description");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(skillId.Name))
            {
                skillId = member;
            }
            else if (member.NameEquals(description.Name))
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
            SkillId = skillId.Select(value => value.GetInt32()),
            Description = description.Select(value => value.GetString()) ?? ""
        };
    }
}
