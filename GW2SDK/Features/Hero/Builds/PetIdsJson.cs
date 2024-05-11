using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class PetIdsJson
{
    public static (int? PetId1, int? PetId2) GetPetIds(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return (first.GetNullableInt32(), second.GetNullableInt32());
    }
}
