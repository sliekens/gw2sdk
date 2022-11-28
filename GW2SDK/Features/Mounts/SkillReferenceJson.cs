using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Mounts;

[PublicAPI]
public static class SkillReferenceJson
{
    public static SkillReference GetSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }
}
