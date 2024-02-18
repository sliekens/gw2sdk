using System.Text.Json;
using GuildWars2.Hero.Races;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class RestrictionsJson
{
    public static IReadOnlyList<RaceName> GetRestrictions(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        List<RaceName>? races = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(RaceName.Asura)))
            {
                races ??= [];
                races.Add(RaceName.Asura);
            }
            else if (entry.ValueEquals(nameof(RaceName.Charr)))
            {
                races ??= [];
                races.Add(RaceName.Charr);
            }
            else if (entry.ValueEquals(nameof(RaceName.Human)))
            {
                races ??= [];
                races.Add(RaceName.Human);
            }
            else if (entry.ValueEquals(nameof(RaceName.Norn)))
            {
                races ??= [];
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= [];
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= [];
                races.Add(RaceName.Norn);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedEnum(entry.GetRawText()));
            }
        }

        return races ?? Race.AllRaces;
    }
}
