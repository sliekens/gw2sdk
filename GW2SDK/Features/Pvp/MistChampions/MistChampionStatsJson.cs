using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionStatsJson
{
    public static MistChampionStats GetMistChampionStats(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MistChampionStats
        {
            Offense = offense.Map(static value => value.GetInt32()),
            Defense = defense.Map(static value => value.GetInt32()),
            Speed = speed.Map(static value => value.GetInt32())
        };
    }
}
