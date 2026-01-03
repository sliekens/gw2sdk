using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedSpecializationsJson
{
    public static (SelectedSpecialization? Specialization1, SelectedSpecialization? Specialization2,
        SelectedSpecialization? Specialization3) GetSelectedSpecializations(this in JsonElement json)
    {
        JsonElement first = default;
        JsonElement second = default;
        JsonElement third = default;

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
            else if (third.ValueKind == JsonValueKind.Undefined)
            {
                third = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return (first.GetSelectedSpecialization(), second.GetSelectedSpecialization(),
            third.GetSelectedSpecialization());
    }
}
