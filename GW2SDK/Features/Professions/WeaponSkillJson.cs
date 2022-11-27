using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");
        NullableMember<Offhand> offhand = new("offhand");
        NullableMember<Attunement> attunement = new("attunement");

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
            else if (member.NameEquals(offhand.Name))
            {
                offhand.Value = member.Value;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            Offhand = offhand.GetValue(missingMemberBehavior),
            Attunement = attunement.GetValue(missingMemberBehavior)
        };
    }
}
