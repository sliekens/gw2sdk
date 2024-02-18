using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;
using static GuildWars2.Hero.ProfessionName;
using static GuildWars2.Hero.RaceName;
using static GuildWars2.Hero.BodyType;

namespace GuildWars2.Items;

internal static class RestrictionsJson
{
    public static (IReadOnlyList<RaceName> Races, IReadOnlyList<ProfessionName> Professions,
        IReadOnlyList<BodyType> BodyTypes) GetRestrictions(
            this JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
    {
        List<RaceName>? races = null;
        List<ProfessionName>? professions = null;
        List<BodyType>? bodyTypes = null;
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedEnum(entry.GetRawText()));
            }
        }

        return (races ?? Race.AllRaces, professions ?? Profession.AllProfessions,
            bodyTypes ?? Character.AllBodyTypes);
    }
}
