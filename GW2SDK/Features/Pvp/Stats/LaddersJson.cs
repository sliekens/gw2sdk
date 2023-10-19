using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

[PublicAPI]
public static class LaddersJson
{
    public static Ladders GetLadders(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember none = new("none");
        OptionalMember unranked = new("unranked");
        OptionalMember ranked = new("ranked");
        OptionalMember ranked2v2 = new("2v2ranked");
        OptionalMember ranked3v3 = new("3v3ranked");
        OptionalMember soloArenaRated = new("soloarenarated");
        OptionalMember teamArenaRated = new("teamarenarated");


        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(none.Name))
            {
                none = member;
            }
            else if (member.NameEquals(unranked.Name))
            {
                unranked = member;
            }
            else if (member.NameEquals(ranked.Name))
            {
                ranked = member;
            }
            else if (member.NameEquals(ranked2v2.Name))
            {
                ranked2v2 = member;
            }
            else if (member.NameEquals(ranked3v3.Name))
            {
                ranked3v3 = member;
            }
            else if (member.NameEquals(soloArenaRated.Name))
            {
                soloArenaRated = member;
            }
            else if (member.NameEquals(teamArenaRated.Name))
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
            None = none.Select(value => value.GetResults(missingMemberBehavior)),
            Unranked = unranked.Select(value => value.GetResults(missingMemberBehavior)),
            Ranked = ranked.Select(value => value.GetResults(missingMemberBehavior)),
            Ranked2v2 = ranked2v2.Select(value => value.GetResults(missingMemberBehavior)),
            Ranked3v3 = ranked3v3.Select(value => value.GetResults(missingMemberBehavior)),
            SoloArenaRated = soloArenaRated.Select(value => value.GetResults(missingMemberBehavior)),
            TeamArenaRated = teamArenaRated.Select(value => value.GetResults(missingMemberBehavior))
        };
    }
}
