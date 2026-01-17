using System.Text.Json;

using GuildWars2.Hero;
using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;

using static GuildWars2.Hero.BodyType;
using static GuildWars2.Hero.ProfessionName;
using static GuildWars2.Hero.RaceName;

namespace GuildWars2.Items;

internal static class ItemRestrictionJson
{
    public static ItemRestriction GetItemRestriction(this in JsonElement json)
    {
        ImmutableList<Extensible<RaceName>>.Builder? races = null;
        ImmutableList<Extensible<ProfessionName>>.Builder? professions = null;
        ImmutableList<Extensible<BodyType>>.Builder? bodyTypes = null;
        ImmutableList<string>.Builder other = ImmutableList.CreateBuilder<string>();
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(Asura)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Asura);
            }
            else if (entry.ValueEquals(nameof(Charr)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Charr);
            }
            else if (entry.ValueEquals(nameof(Human)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Human);
            }
            else if (entry.ValueEquals(nameof(Norn)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= ImmutableList.CreateBuilder<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Elementalist)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Elementalist);
            }
            else if (entry.ValueEquals(nameof(Engineer)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Engineer);
            }
            else if (entry.ValueEquals(nameof(Guardian)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Guardian);
            }
            else if (entry.ValueEquals(nameof(Mesmer)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Mesmer);
            }
            else if (entry.ValueEquals(nameof(Necromancer)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Necromancer);
            }
            else if (entry.ValueEquals(nameof(Ranger)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Ranger);
            }
            else if (entry.ValueEquals(nameof(Revenant)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Revenant);
            }
            else if (entry.ValueEquals(nameof(Thief)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Thief);
            }
            else if (entry.ValueEquals(nameof(Warrior)))
            {
                professions ??= ImmutableList.CreateBuilder<Extensible<ProfessionName>>();
                professions.Add(Warrior);
            }
            else if (entry.ValueEquals(nameof(Female)))
            {
                bodyTypes ??= ImmutableList.CreateBuilder<Extensible<BodyType>>();
                bodyTypes.Add(Female);
            }
            else if (entry.ValueEquals(nameof(Male)))
            {
                bodyTypes ??= ImmutableList.CreateBuilder<Extensible<BodyType>>();
                bodyTypes.Add(Male);
            }
            else
            {
                string? restriction = entry.GetString();
                if (!string.IsNullOrEmpty(restriction))
                {
                    other.Add(restriction!);
                }
            }
        }

        return new ItemRestriction
        {
            Races = races is not null
                ? new ImmutableValueList<Extensible<RaceName>>(races.ToImmutable())
                : Race.AllRaces,
            Professions = professions is not null
                ? new ImmutableValueList<Extensible<ProfessionName>>(professions.ToImmutable())
                : Profession.AllProfessions,
            BodyTypes = bodyTypes is not null
                ? new ImmutableValueList<Extensible<BodyType>>(bodyTypes.ToImmutable())
                : Character.AllBodyTypes,
            Other = new ImmutableValueList<string>(other.ToImmutable())
        };
    }
}
