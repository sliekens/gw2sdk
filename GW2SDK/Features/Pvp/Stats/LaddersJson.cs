using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

internal static class LaddersJson
{
    public static Ladders GetLadders(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember none = "none";
        OptionalMember unranked = "unranked";
        OptionalMember ranked = "ranked";
        OptionalMember ranked2v2 = "2v2ranked";
        OptionalMember ranked3v3 = "3v3ranked";
        OptionalMember soloArenaRated = "soloarenarated";
        OptionalMember teamArenaRated = "teamarenarated";

        foreach (var member in json.EnumerateObject())
        {
            if (none.Match(member))
            {
                none = member;
            }
            else if (unranked.Match(member))
            {
                unranked = member;
            }
            else if (ranked.Match(member))
            {
                ranked = member;
            }
            else if (ranked2v2.Match(member))
            {
                ranked2v2 = member;
            }
            else if (ranked3v3.Match(member))
            {
                ranked3v3 = member;
            }
            else if (soloArenaRated.Match(member))
            {
                soloArenaRated = member;
            }
            else if (teamArenaRated.Match(member))
            {
                teamArenaRated = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ladders
        {
            None = none.Map(value => value.GetResults(missingMemberBehavior)),
            Unranked = unranked.Map(value => value.GetResults(missingMemberBehavior)),
            Ranked = ranked.Map(value => value.GetResults(missingMemberBehavior)),
            Ranked2v2 = ranked2v2.Map(value => value.GetResults(missingMemberBehavior)),
            Ranked3v3 = ranked3v3.Map(value => value.GetResults(missingMemberBehavior)),
            SoloArenaRated = soloArenaRated.Map(value => value.GetResults(missingMemberBehavior)),
            TeamArenaRated = teamArenaRated.Map(value => value.GetResults(missingMemberBehavior))
        };
    }
}
