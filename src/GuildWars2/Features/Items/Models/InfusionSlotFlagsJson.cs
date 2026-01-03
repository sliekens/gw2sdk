using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotFlagsJson
{
    public static InfusionSlotFlags GetInfusionSlotFlags(this in JsonElement json)
    {
        bool enrichment = false;
        bool infusion = false;
        ValueList<string> others = [];
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Enrichment"))
            {
                enrichment = true;
            }
            else if (entry.ValueEquals("Infusion"))
            {
                infusion = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new InfusionSlotFlags
        {
            Enrichment = enrichment,
            Infusion = infusion,
            Other = others
        };
    }
}
