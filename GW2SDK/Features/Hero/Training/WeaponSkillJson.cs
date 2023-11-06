using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";
        NullableMember offhand = "offhand";
        NullableMember attunement = "attunement";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == offhand.Name)
            {
                offhand = member;
            }
            else if (member.Name == attunement.Name)
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
            Id = id.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            Offhand = offhand.Map(value => value.GetEnum<Offhand>(missingMemberBehavior)),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior))
        };
    }
}
