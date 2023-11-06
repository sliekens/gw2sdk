using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Stories;

internal static class SeasonJson
{
    public static Season GetSeason(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember order = "order";
        RequiredMember stories = "stories";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == stories.Name)
            {
                stories = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Season
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            StoryIds = stories.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
