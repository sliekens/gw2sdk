using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class PvpEquipmentJson
{
    public static PvpEquipment GetPvpEquipment(this JsonElement json)
    {
        NullableMember amulet = "amulet";
        NullableMember rune = "rune";
        RequiredMember sigils = "sigils";

        foreach (var member in json.EnumerateObject())
        {
            if (amulet.Match(member))
            {
                amulet = member;
            }
            else if (rune.Match(member))
            {
                rune = member;
            }
            else if (sigils.Match(member))
            {
                sigils = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new PvpEquipment
        {
            AmuletId = amulet.Map(static value => value.GetInt32()),
            RuneId = rune.Map(static value => value.GetInt32()),
            SigilIds = sigils.Map(static values =>
                values.GetList(static value => value.GetNullableInt32())
            )
        };
    }
}
