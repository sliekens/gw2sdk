using System.Text.Json;

using GuildWars2.Collections;
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
        ValueList<Extensible<RaceName>>? races = null;
        ValueList<Extensible<ProfessionName>>? professions = null;
        ValueList<Extensible<BodyType>>? bodyTypes = null;
        ValueList<string> other = [];
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(Asura)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Asura);
            }
            else if (entry.ValueEquals(nameof(Charr)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Charr);
            }
            else if (entry.ValueEquals(nameof(Human)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Human);
            }
            else if (entry.ValueEquals(nameof(Norn)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= new ValueList<Extensible<RaceName>>();
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Elementalist)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Elementalist);
            }
            else if (entry.ValueEquals(nameof(Engineer)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Engineer);
            }
            else if (entry.ValueEquals(nameof(Guardian)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Guardian);
            }
            else if (entry.ValueEquals(nameof(Mesmer)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Mesmer);
            }
            else if (entry.ValueEquals(nameof(Necromancer)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Necromancer);
            }
            else if (entry.ValueEquals(nameof(Ranger)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Ranger);
            }
            else if (entry.ValueEquals(nameof(Revenant)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Revenant);
            }
            else if (entry.ValueEquals(nameof(Thief)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Thief);
            }
            else if (entry.ValueEquals(nameof(Warrior)))
            {
                professions ??= new ValueList<Extensible<ProfessionName>>();
                professions.Add(Warrior);
            }
            else if (entry.ValueEquals(nameof(Female)))
            {
                bodyTypes ??= new ValueList<Extensible<BodyType>>();
                bodyTypes.Add(Female);
            }
            else if (entry.ValueEquals(nameof(Male)))
            {
                bodyTypes ??= new ValueList<Extensible<BodyType>>();
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
            Races = races ?? Race.AllRaces,
            Professions = professions ?? Profession.AllProfessions,
            BodyTypes = Character.AllBodyTypes,
            Other = other
        };
    }
}
