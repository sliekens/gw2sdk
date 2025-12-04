using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";
        NullableMember offhand = "offhand";
        NullableMember attunement = "attunement";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (offhand.Match(member))
            {
                offhand = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new WeaponSkill
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Slot = slot.Map(static (in value) => value.GetEnum<SkillSlot>()),
            Offhand = offhand.Map(static (in value) => value.GetEnum<Offhand>()),
            Attunement = attunement.Map(static (in value) => value.GetEnum<Attunement>())
        };
    }
}
