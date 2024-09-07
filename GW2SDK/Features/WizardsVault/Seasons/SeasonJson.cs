using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Season
        {
            Title = title.Map(static value => value.GetStringRequired()),
            Start = start.Map(static value => value.GetDateTimeOffset()),
            End = end.Map(static value => value.GetDateTimeOffset()),
            AstralRewardIds =
                listings.Map(static values => values.GetSet(static value => value.GetInt32())),
            ObjectiveIds =
                objectives.Map(static values => values.GetSet(static value => value.GetInt32()))
        };
    }
}
