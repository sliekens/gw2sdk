using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;
using static GuildWars2.Hero.ProfessionName;
using static GuildWars2.Hero.RaceName;
using static GuildWars2.Hero.BodyType;

namespace GuildWars2.Items;

internal static class ItemRestrictionJson
{
    public static ItemRestriction GetItemRestriction(
        this JsonElement json
    )
    {
        List<Extensible<RaceName>>? races = null;
        List<Extensible<ProfessionName>>? professions = null;
        List<Extensible<BodyType>>? bodyTypes = null;
        List<string>? other = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals(nameof(Asura)))
            {
                races ??= [];
                races.Add(Asura);
            }
            else if (entry.ValueEquals(nameof(Charr)))
            {
                races ??= [];
                races.Add(Charr);
            }
            else if (entry.ValueEquals(nameof(Human)))
            {
                races ??= [];
                races.Add(Human);
            }
            else if (entry.ValueEquals(nameof(Norn)))
            {
                races ??= [];
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= [];
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Sylvari)))
            {
                races ??= [];
                races.Add(Norn);
            }
            else if (entry.ValueEquals(nameof(Elementalist)))
            {
                professions ??= [];
                professions.Add(Elementalist);
            }
            else if (entry.ValueEquals(nameof(Engineer)))
            {
                professions ??= [];
                professions.Add(Engineer);
            }
            else if (entry.ValueEquals(nameof(Guardian)))
            {
                professions ??= [];
                professions.Add(Guardian);
            }
            else if (entry.ValueEquals(nameof(Mesmer)))
            {
                professions ??= [];
                professions.Add(Mesmer);
            }
            else if (entry.ValueEquals(nameof(Necromancer)))
            {
                professions ??= [];
                professions.Add(Necromancer);
            }
            else if (entry.ValueEquals(nameof(Ranger)))
            {
                professions ??= [];
                professions.Add(Ranger);
            }
            else if (entry.ValueEquals(nameof(Revenant)))
            {
                professions ??= [];
                professions.Add(Revenant);
            }
            else if (entry.ValueEquals(nameof(Thief)))
            {
                professions ??= [];
                professions.Add(Thief);
            }
            else if (entry.ValueEquals(nameof(Warrior)))
            {
                professions ??= [];
                professions.Add(Warrior);
            }
            else if (entry.ValueEquals(nameof(Female)))
            {
                bodyTypes ??= [];
                bodyTypes.Add(Female);
            }
            else if (entry.ValueEquals(nameof(Male)))
            {
                bodyTypes ??= [];
                bodyTypes.Add(Male);
            }
            else
            {
                var restriction = entry.GetString();
                if (!string.IsNullOrEmpty(restriction))
                {
                    other ??= [];
                    other.Add(restriction!);
                }
            }
        }

        return new ItemRestriction
        {
            Races = races ?? Race.AllRaces,
            Professions = professions ?? Profession.AllProfessions,
            BodyTypes = Character.AllBodyTypes,
            Other = other ?? Empty.ListOfString
        };
    }
}
