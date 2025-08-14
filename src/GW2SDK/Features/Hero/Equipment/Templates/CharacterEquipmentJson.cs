using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class CharacterEquipmentJson
{
    public static CharacterEquipment GetCharacterEquipment(this in JsonElement json)
    {
        RequiredMember items = "equipment";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (items.Match(member))
            {
                items = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CharacterEquipment
        {
            Items = items.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetEquipmentItem())
            )
        };
    }
}
