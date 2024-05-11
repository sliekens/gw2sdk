using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class AllWorldsJson
{
    public static AllWorlds GetAllWorlds(this JsonElement json)
    {
        RequiredMember red = "red";
        RequiredMember blue = "blue";
        RequiredMember green = "green";

        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AllWorlds
        {
            Red = red.Map(static values => values.GetList(static value => value.GetInt32())),
            Blue = blue.Map(static values => values.GetList(static value => value.GetInt32())),
            Green = green.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
