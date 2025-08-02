using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class BuffJson
{
    public static Buff GetBuff(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Buff
        {
            SkillId = skillId.Map(static (in JsonElement value) => value.GetInt32()),
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? ""
        };
    }
}
