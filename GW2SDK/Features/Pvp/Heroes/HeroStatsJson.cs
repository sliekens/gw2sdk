using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Heroes;

internal static class HeroStatsJson
{
    public static HeroStats GetHeroStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember offense = "offense";
        RequiredMember defense = "defense";
        RequiredMember speed = "speed";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == offense.Name)
            {
                offense = member;
            }
            else if (member.Name == defense.Name)
            {
                defense = member;
            }
            else if (member.Name == speed.Name)
            {
                speed = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HeroStats
        {
            Offense = offense.Map(value => value.GetInt32()),
            Defense = defense.Map(value => value.GetInt32()),
            Speed = speed.Map(value => value.GetInt32())
        };
    }
}
