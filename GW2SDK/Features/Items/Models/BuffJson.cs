using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class BuffJson
{
    public static Buff GetBuff(this JsonElement value, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> skillId = new("skill_id");
        OptionalMember<string> description = new("description");
        foreach (var member in value.EnumerateObject())
        {
            if (member.NameEquals(skillId.Name))
            {
                skillId.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Buff
        {
            SkillId = skillId.GetValue(),
            Description = description.GetValueOrEmpty()
        };
    }
}
