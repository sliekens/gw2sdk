using System.Drawing;
using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration;

internal static class SizeJson
{
    public static Size GetDimensions(this in JsonElement json)
    {
        JsonElement width = default;
        JsonElement height = default;

        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (width.ValueKind == JsonValueKind.Undefined)
            {
                width = entry;
            }
            else if (height.ValueKind == JsonValueKind.Undefined)
            {
                height = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return new Size(width.GetInt32(), height.GetInt32());
    }
}
