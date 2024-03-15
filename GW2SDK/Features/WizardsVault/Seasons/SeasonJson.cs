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
            if (title.Match(member))
            {
                title = member;
            }
            else if (start.Match(member))
            {
                start = member;
            }
            else if (end.Match(member))
            {
                end = member;
            }
            else if (listings.Match(member))
            {
                listings = member;
            }
            else if (objectives.Match(member))
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
