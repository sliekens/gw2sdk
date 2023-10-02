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
        OptionalMember<Results> none = new("none");
        OptionalMember<Results> unranked = new("unranked");
        OptionalMember<Results> ranked = new("ranked");
        OptionalMember<Results> ranked2v2 = new("2v2ranked");
        OptionalMember<Results> ranked3v3 = new("3v3ranked");
        OptionalMember<Results> soloArenaRated = new("soloarenarated");
        OptionalMember<Results> teamArenaRated = new("teamarenarated");


        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(none.Name))
            {
                none.Value = member.Value;
            }
            else if (member.NameEquals(unranked.Name))
            {
                unranked.Value = member.Value;
            }
            else if (member.NameEquals(ranked.Name))
            {
                ranked.Value = member.Value;
            }
            else if (member.NameEquals(ranked2v2.Name))
            {
                ranked2v2.Value = member.Value;
            }
            else if (member.NameEquals(ranked3v3.Name))
            {
                ranked3v3.Value = member.Value;
            }
            else if (member.NameEquals(soloArenaRated.Name))
            {
                soloArenaRated.Value = member.Value;
            }
            else if (member.NameEquals(teamArenaRated.Name))
            {
                teamArenaRated.Value = member.Value;
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
