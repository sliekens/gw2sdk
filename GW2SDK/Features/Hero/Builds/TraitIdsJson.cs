using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitIdsJson
{
    public static (int? TraitId, int? TraitId2, int? TraitId3) GetTraitIds(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return (first.GetNullableInt32(), second.GetNullableInt32(), third.GetNullableInt32());
    }
}
