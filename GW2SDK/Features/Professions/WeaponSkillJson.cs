using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember slot = new("slot");
        NullableMember offhand = new("offhand");
        NullableMember attunement = new("attunement");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = member;
            }
            else if (member.NameEquals(offhand.Name))
            {
                offhand = member;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.Select(value => value.GetInt32()),
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            Offhand = offhand.Select(value => value.GetEnum<Offhand>(missingMemberBehavior)),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior))
        };
    }
}
