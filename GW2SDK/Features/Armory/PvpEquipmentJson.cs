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
        NullableMember<int> amulet = new("amulet");
        NullableMember<int> rune = new("rune");
        RequiredMember<int?> sigils = new("sigils");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(amulet.Name))
            {
                amulet.Value = member.Value;
            }
            else if (member.NameEquals(rune.Name))
            {
                rune.Value = member.Value;
            }
            else if (member.NameEquals(sigils.Name))
            {
                sigils.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PvpEquipment
        {
            AmuletId = amulet.GetValue(),
            RuneId = rune.GetValue(),
            SigilIds = sigils.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
