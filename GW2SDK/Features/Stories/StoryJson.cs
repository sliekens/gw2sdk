using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class StoryJson
{
    public static Story GetStory(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember season = new("season");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember timeline = new("timeline");
        RequiredMember level = new("level");
        OptionalMember races = new("races");
        RequiredMember order = new("order");
        RequiredMember chapters = new("chapters");
        OptionalMember flags = new("flags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(season.Name))
            {
                season.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(timeline.Name))
            {
                timeline.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(races.Name))
            {
                races.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(chapters.Name))
            {
                chapters.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Story
        {
            Id = id.Select(value => value.GetInt32()),
            SeasonId = season.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Timeline = timeline.Select(value => value.GetStringRequired()),
            Level = level.Select(value => value.GetInt32()),
            Races = races.SelectMany(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Order = order.Select(value => value.GetInt32()),
            Chapters = chapters.SelectMany(value => value.GetChapter(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<StoryFlag>(missingMemberBehavior)) ?? Array.Empty<StoryFlag>()
        };
    }
}
