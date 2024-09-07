using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class UtilitySkillIdsJson
{
    public static (int? UtilitySkillId, int? UtilitySkillId2, int? UtilitySkillId3)
        GetUtilitySkillIds(this JsonElement json)
    {
        JsonElement first = default;
        JsonElement second = default;
        JsonElement third = default;

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
            else if (third.ValueKind == JsonValueKind.Undefined)
            {
                third = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return (first.GetNullableInt32(), second.GetNullableInt32(), third.GetNullableInt32());
    }
}
