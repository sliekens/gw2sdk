using System.Text.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedSpecializationsJson
{
    public static (SelectedSpecialization? Specialization1, SelectedSpecialization? Specialization2, SelectedSpecialization? Specialization3) GetSelectedSpecializations(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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

        return (first.GetSelectedSpecialization(missingMemberBehavior), second.GetSelectedSpecialization(missingMemberBehavior), third.GetSelectedSpecialization(missingMemberBehavior));
    }
}
