using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class DistributionJson
{
    public static Distribution GetDistribution(this in JsonElement json)
    {
        RequiredMember red = "red";
        RequiredMember blue = "blue";
        RequiredMember green = "green";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (red.Match(member))
            {
                red = member;
            }
            else if (blue.Match(member))
            {
                blue = member;
            }
            else if (green.Match(member))
            {
                green = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Distribution
        {
            Red = red.Map(static (in value) => value.GetInt32()),
            Blue = blue.Map(static (in value) => value.GetInt32()),
            Green = green.Map(static (in value) => value.GetInt32())
        };
    }
}
