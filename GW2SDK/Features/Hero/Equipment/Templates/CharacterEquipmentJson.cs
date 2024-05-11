using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class CharacterEquipmentJson
{
    public static CharacterEquipment GetCharacterEquipment(this JsonElement json)
    {
        RequiredMember items = "equipment";

        foreach (var member in json.EnumerateObject())
        {
            if (items.Match(member))
            {
                items = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterEquipment
        {
            Items = items.Map(
                static values => values.GetList(static value => value.GetEquipmentItem())
            )
        };
    }
}
