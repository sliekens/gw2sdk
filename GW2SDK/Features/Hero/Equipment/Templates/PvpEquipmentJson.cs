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
            if (member.Name == amulet.Name)
            {
                amulet = member;
            }
            else if (member.Name == rune.Name)
            {
                rune = member;
            }
            else if (member.Name == sigils.Name)
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
