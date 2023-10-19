using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class SeasonJson
{
    public static Season GetSeason(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember wins = new("wins");
        RequiredMember losses = new("losses");
        RequiredMember rating = new("rating");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(wins.Name))
            {
                wins.Value = member.Value;
            }
            else if (member.NameEquals(losses.Name))
            {
                losses.Value = member.Value;
            }
            else if (member.NameEquals(rating.Name))
            {
                rating.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Season
        {
            Id = id.Select(value => value.GetStringRequired()),
            Wins = wins.Select(value => value.GetInt32()),
            Losses = losses.Select(value => value.GetInt32()),
            Rating = rating.Select(value => value.GetInt32())
        };
    }
}
