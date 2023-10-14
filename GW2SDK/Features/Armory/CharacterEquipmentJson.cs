﻿using System.Text.Json;
using GuildWars2.Armory;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class CharacterEquipmentJson
{
    public static CharacterEquipment GetCharacterEquipment(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<EquipmentItem> equipment = new("equipment");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(equipment.Name))
            {
                equipment.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterEquipment
        {
            Equipment =
                equipment.SelectMany(value => value.GetEquipmentItem(missingMemberBehavior))
        };
    }
}