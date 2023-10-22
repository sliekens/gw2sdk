using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Exploration;

internal static class SizeJson
{
    public static Size GetDimensions(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement width = default;
        JsonElement height = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (width.ValueKind == JsonValueKind.Undefined)
            {
                width = entry;
            }
            else if (height.ValueKind == JsonValueKind.Undefined)
            {
                height = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return new Size(width.GetInt32(), height.GetInt32());
    }
}
