using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionStatsJson
{
    public static MistChampionStats GetMistChampionStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember offense = "offense";
        RequiredMember defense = "defense";
        RequiredMember speed = "speed";

        foreach (var member in json.EnumerateObject())
        {
            if (offense.Match(member))
            {
                offense = member;
            }
            else if (defense.Match(member))
            {
                defense = member;
            }
            else if (speed.Match(member))
            {
                speed = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MistChampionStats
        {
            Offense = offense.Map(value => value.GetInt32()),
            Defense = defense.Map(value => value.GetInt32()),
            Speed = speed.Map(value => value.GetInt32())
        };
    }
}
