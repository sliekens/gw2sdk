using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemFlagsJson
{
    public static GuildEmblemFlags GetGuildEmblemFlags(this JsonElement json)
    {
        var flipBackgroundHorizontal = false;
        var flipBackgroundVertical = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("FlipBackgroundHorizontal"))
            {
                flipBackgroundHorizontal = true;
            }
            else if (entry.ValueEquals("FlipBackgroundVertical"))
            {
                flipBackgroundVertical = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new GuildEmblemFlags
        {
            FlipBackgroundHorizontal = flipBackgroundHorizontal,
            FlipBackgroundVertical = flipBackgroundVertical,
            Other = others
        };
    }
}
