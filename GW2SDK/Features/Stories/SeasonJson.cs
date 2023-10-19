using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class SeasonJson
{
    public static Season GetSeason(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember order = new("order");
        RequiredMember stories = new("stories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(stories.Name))
            {
                stories.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Season
        {
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            StoryIds = stories.SelectMany(value => value.GetInt32())
        };
    }
}
