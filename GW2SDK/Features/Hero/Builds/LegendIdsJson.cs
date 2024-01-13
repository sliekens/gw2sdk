using System.Text.Json;

namespace GuildWars2.Hero.Builds;

internal static class LegendIdsJson
{
    public static (string? LegendId, string? LegendId2) GetLegendIds(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement first = default;
        JsonElement second = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (first.ValueKind == JsonValueKind.Undefined)
            {
                first = entry;
            }
            else if (second.ValueKind == JsonValueKind.Undefined)
            {
                second = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return (first.GetString(), second.GetString());
    }
}
