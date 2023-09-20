using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class DistributionJson
{
    public static Distribution GetDistribution(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> red = new("red");
        RequiredMember<int> blue = new("blue");
        RequiredMember<int> green = new("green");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(red.Name))
            {
                red.Value = member.Value;
            }
            else if (member.NameEquals(blue.Name))
            {
                blue.Value = member.Value;
            }
            else if (member.NameEquals(green.Name))
            {
                green.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Distribution
        {
            Red = red.GetValue(),
            Blue = blue.GetValue(),
            Green = green.GetValue()
        };
    }
}
