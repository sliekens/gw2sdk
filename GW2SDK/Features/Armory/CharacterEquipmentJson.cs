using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Armory;

[PublicAPI]
public static class CharacterEquipmentJson
{
    public static CharacterEquipment GetCharacterEquipment(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember equipment = "equipment";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(equipment.Name))
            {
                equipment = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterEquipment
        {
            Equipment =
                equipment.Select(values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior)))
        };
    }
}
