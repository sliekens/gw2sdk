using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(this in JsonElement json)
    {
        RequiredMember title = "title";
        RequiredMember start = "start";
        RequiredMember end = "end";
        RequiredMember listings = "listings";
        RequiredMember objectives = "objectives";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Title = title.Map(static (in value) => value.GetStringRequired()),
            Start = start.Map(static (in value) => value.GetDateTimeOffset()),
            End = end.Map(static (in value) => value.GetDateTimeOffset()),
            AstralRewardIds =
                listings.Map(static (in values) => values.GetSet(static (in value) => value.GetInt32())),
            ObjectiveIds =
                objectives.Map(static (in values) => values.GetSet(static (in value) => value.GetInt32()))
        };
    }
}
