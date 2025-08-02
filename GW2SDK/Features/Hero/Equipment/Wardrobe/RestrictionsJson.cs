using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Hero.Races;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class RestrictionsJson
{
    public static IReadOnlyList<Extensible<RaceName>> GetRestrictions(this in JsonElement json)
    {
        ValueList<Extensible<RaceName>>? races = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(RaceName.Asura)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Asura);
            }
            else if (entry.ValueEquals(nameof(RaceName.Charr)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Charr);
            }
            else if (entry.ValueEquals(nameof(RaceName.Human)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Human);
            }
            else if (entry.ValueEquals(nameof(RaceName.Norn)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else
            {
                var restriction = entry.GetString();
                if (!string.IsNullOrEmpty(restriction))
                {
                    races ??= new ValueList<Extensible<RaceName>>();
                    races.Add(new Extensible<RaceName>(restriction!));
                }
            }
        }

        return races ?? Race.AllRaces;
    }
}
