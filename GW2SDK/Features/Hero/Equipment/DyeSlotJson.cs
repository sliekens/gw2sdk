﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class DyeSlotJson
{
    public static DyeSlot GetDyeSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember colorId = "color_id";
        RequiredMember material = "material";
        foreach (var member in json.EnumerateObject())
        {
            if (colorId.Match(member))
            {
                colorId = member;
            }
            else if (material.Match(member))
            {
                material = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DyeSlot
        {
            ColorId = colorId.Map(value => value.GetInt32()),
            Material = material.Map(value => value.GetEnum<Material>(missingMemberBehavior))
        };
    }
}
