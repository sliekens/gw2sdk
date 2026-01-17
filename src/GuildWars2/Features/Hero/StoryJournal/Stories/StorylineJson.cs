using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StorylineJson
{
    public static Storyline GetStoryline(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember order = "order";
        RequiredMember stories = "stories";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (stories.Match(member))
            {
                stories = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Storyline
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Order = order.Map(static (in value) => value.GetInt32()),
            StoryIds = stories.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
        };
    }
}
