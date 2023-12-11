using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class DivisionFlagsJson
{
    public static DivisionFlags GetDivisionFlags(this JsonElement json)
    {
        var canLosePoints = false;
        var canLoseTiers = false;
        var repeatable = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("CanLosePoints"))
            {
                canLosePoints = true;
            }
            else if (entry.ValueEquals("CanLoseTiers"))
            {
                canLoseTiers = true;
            }
            else if (entry.ValueEquals("Repeatable"))
            {
                repeatable = true;
            }
            else
            {
                others ??= [];
                others.Add(entry.GetStringRequired());
            }
        }

        return new DivisionFlags
        {
            CanLosePoints = canLosePoints,
            CanLoseTiers = canLoseTiers,
            Repeatable = repeatable,
            Other = others ?? Empty.ListOfString
        };
    }
}
