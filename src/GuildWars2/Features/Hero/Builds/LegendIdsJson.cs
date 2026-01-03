using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class LegendIdsJson
{
    public static (string? LegendId1, string? LegendId2) GetLegendIds(
        this in JsonElement json
    )
    {
        JsonElement first = default;
        JsonElement second = default;

        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (first.ValueKind == JsonValueKind.Undefined)
            {
                first = entry;
            }
            else if (second.ValueKind == JsonValueKind.Undefined)
            {
                second = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return (first.GetString(), second.GetString());
    }
}
