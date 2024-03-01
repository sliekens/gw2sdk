using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember title = "title";
        RequiredMember start = "start";
        RequiredMember end = "end";
        RequiredMember listings = "listings";
        RequiredMember objectives = "objectives";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == title.Name)
            {
                title = member;
            }
            else if (member.Name == start.Name)
            {
                start = member;
            }
            else if (member.Name == end.Name)
            {
                end = member;
            }
            else if (member.Name == listings.Name)
            {
                listings = member;
            }
            else if (member.Name == objectives.Name)
            {
                objectives = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Season
        {
            Title = title.Map(value => value.GetStringRequired()),
            Start = start.Map(value => value.GetDateTimeOffset()),
            End = end.Map(value => value.GetDateTimeOffset()),
            AstralRewardIds = listings.Map(values => values.GetSet(value => value.GetInt32())),
            ObjectiveIds = objectives.Map(values => values.GetSet(value => value.GetInt32()))
        };
    }
}
