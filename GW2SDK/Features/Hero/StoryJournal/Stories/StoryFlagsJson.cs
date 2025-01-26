using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StoryFlagsJson
{
    public static StoryFlags GetStoryFlags(this JsonElement json)
    {
        var requiresUnlock = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("RequiresUnlock"))
            {
                requiresUnlock = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new StoryFlags
        {
            RequiresUnlock = requiresUnlock,
            Other = others
        };
    }
}
