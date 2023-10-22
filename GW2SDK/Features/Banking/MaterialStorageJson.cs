﻿using System.Text.Json;

namespace GuildWars2.Banking;

internal static class MaterialStorageJson
{
    public static MaterialStorage GetMaterialStorage(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        List<MaterialSlot> slots = new(json.GetArrayLength());

        slots.AddRange(
            json.EnumerateArray().Select(entry => entry.GetMaterialSlot(missingMemberBehavior))
        );

        return new MaterialStorage(slots);
    }
}
