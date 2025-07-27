using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal static class SkillReferenceJson
{
    public static SkillReference GetSkillReference(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SkillReference
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<SkillSlot>())
        };
    }
}
