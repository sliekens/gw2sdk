using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class PvpEquipmentJson
{
    public static PvpEquipment GetPvpEquipment(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PvpEquipment
        {
            AmuletId = amulet.Map(value => value.GetInt32()),
            RuneId = rune.Map(value => value.GetInt32()),
            SigilIds = sigils.Map(values => values.GetList(value => value.GetNullableInt32()))
        };
    }
}
