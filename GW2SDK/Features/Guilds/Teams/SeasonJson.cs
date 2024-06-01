using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class SeasonJson
{
    public static Season GetSeason(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember wins = "wins";
        RequiredMember losses = "losses";
        RequiredMember rating = "rating";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (wins.Match(member))
            {
                wins = member;
            }
            else if (losses.Match(member))
            {
                losses = member;
            }
            else if (rating.Match(member))
            {
                rating = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Season
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Wins = wins.Map(static value => value.GetInt32()),
            Losses = losses.Map(static value => value.GetInt32()),
            Rating = rating.Map(static value => value.GetInt32())
        };
    }
}
