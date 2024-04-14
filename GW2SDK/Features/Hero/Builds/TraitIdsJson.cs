using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitIdsJson
{
    public static (int? AdeptTraitId, int? MasterTraitId, int? GrandmasterTraitId) GetTraitIds(this JsonElement json)
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
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return (first.GetNullableInt32(), second.GetNullableInt32(), third.GetNullableInt32());
    }
}
