using System.Text.Json;
using GuildWars2.Hero.Builds;
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
