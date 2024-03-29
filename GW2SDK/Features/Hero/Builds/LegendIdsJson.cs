﻿using System.Text.Json;

namespace GuildWars2.Hero.Builds;

internal static class LegendIdsJson
{
    public static (string? LegendId1, string? LegendId2) GetLegendIds(
        this JsonElement json,
        SelectedSpecialization? specialization1,
        SelectedSpecialization? specialization2,
        SelectedSpecialization? specialization3,
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

        return (Legend(first), Legend(second));

        string? Legend(JsonElement value) => value.GetString() switch
        {
            // Normally, legends are represented as Legend1, Legend2, etc., or null if no legend is selected.
            // Unfortunately, build template/storage APIs return meme values instead of legend IDs
            // and null can also mean a Renegade or Vindicator legends was selected
            "Legend1" or "Fire" => "Legend1", // Glint 
            "Legend2" or "Water" => "Legend2", // Shiro 
            "Legend3" or "Air" => "Legend3", // Jalis 
            "Legend4" or "Earth" => "Legend4", // Mallyx 
            "Legend5" => "Legend5", // Kalla 
            "Legend6" or "Deathshroud" => "Legend6", // Ventari 
            null when IsRenegade() => "Legend5", // Kalla 
            null when IsVindicator() => null, // Legend7 (Alliance stance) is not even supported by v2/legends
            null => null,
            _ => throw new InvalidOperationException(
                $"Unexpected legend: {value.GetString()}"
            )
        };

        // https://api.guildwars2.com/v2/specializations/63
        bool IsRenegade() => specialization1?.Id == 63 || specialization2?.Id == 63 || specialization3?.Id == 63;

        // https://api.guildwars2.com/v2/specializations/69
        bool IsVindicator() => specialization1?.Id == 69 || specialization2?.Id == 69 || specialization3?.Id == 69;
    }
}
