﻿using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class ColorJson
{
    public static Color GetColor(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        JsonElement red = default;
        JsonElement green = default;
        JsonElement blue = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (red.ValueKind == JsonValueKind.Undefined)
            {
                red = entry;
            }
            else if (green.ValueKind == JsonValueKind.Undefined)
            {
                green = entry;
            }
            else if (blue.ValueKind == JsonValueKind.Undefined)
            {
                blue = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return Color.FromArgb(red.GetInt32(), green.GetInt32(), blue.GetInt32());
    }
}
