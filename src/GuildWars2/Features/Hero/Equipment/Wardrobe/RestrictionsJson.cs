using System.Text.Json;

using GuildWars2.Hero.Races;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class RestrictionsJson
{
    private static readonly IImmutableValueList<Extensible<RaceName>> AllRaces =
        new ImmutableValueList<Extensible<RaceName>>(Race.AllRaces);

    public static IImmutableValueList<Extensible<RaceName>> GetRestrictions(this in JsonElement json)
    {
        ImmutableList<Extensible<RaceName>>.Builder? races = null;
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(RaceName.Asura)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Asura);
            }
            else if (entry.ValueEquals(nameof(RaceName.Charr)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Charr);
            }
            else if (entry.ValueEquals(nameof(RaceName.Human)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Human);
            }
            else if (entry.ValueEquals(nameof(RaceName.Norn)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else if (entry.ValueEquals(nameof(RaceName.Sylvari)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(RaceName.Norn);
            }
            else
            {
                string? restriction = entry.GetString();
                if (!string.IsNullOrEmpty(restriction))
                {
                    races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                    races.Add(new Extensible<RaceName>(restriction!));
                }
            }
        }

        return races is not null ? new ImmutableValueList<Extensible<RaceName>>(races.ToImmutable()) : AllRaces;
    }
}
