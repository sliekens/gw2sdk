using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Equipment;

internal static class CharacterEquipmentJson
{
    public static CharacterEquipment GetCharacterEquipment(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember items = "equipment";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == items.Name)
            {
                items = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterEquipment
        {
            Items = items.Map(
                values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior))
            )
        };
    }
}
