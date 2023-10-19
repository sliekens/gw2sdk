using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Armory;

[PublicAPI]
public static class PvpEquipmentJson
{
    public static PvpEquipment GetPvpEquipment(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember amulet = new("amulet");
        NullableMember rune = new("rune");
        RequiredMember sigils = new("sigils");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(amulet.Name))
            {
                amulet = member;
            }
            else if (member.NameEquals(rune.Name))
            {
                rune = member;
            }
            else if (member.NameEquals(sigils.Name))
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
            AmuletId = amulet.Select(value => value.GetInt32()),
            RuneId = rune.Select(value => value.GetInt32()),
            SigilIds = sigils.SelectMany<int?>(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
