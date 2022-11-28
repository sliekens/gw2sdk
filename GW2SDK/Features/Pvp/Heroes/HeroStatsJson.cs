using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroStatsJson
{
    public static HeroStats GetHeroStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> offense = new("offense");
        RequiredMember<int> defense = new("defense");
        RequiredMember<int> speed = new("speed");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(offense.Name))
            {
                offense.Value = member.Value;
            }
            else if (member.NameEquals(defense.Name))
            {
                defense.Value = member.Value;
            }
            else if (member.NameEquals(speed.Name))
            {
                speed.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HeroStats
        {
            Offense = offense.GetValue(),
            Defense = defense.GetValue(),
            Speed = speed.GetValue()
        };
    }
}
