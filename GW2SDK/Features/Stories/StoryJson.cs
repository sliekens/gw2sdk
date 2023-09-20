using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class StoryJson
{
    public static Story GetStory(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> season = new("season");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> timeline = new("timeline");
        RequiredMember<int> level = new("level");
        OptionalMember<RaceName> races = new("races");
        RequiredMember<int> order = new("order");
        RequiredMember<Chapter> chapters = new("chapters");
        OptionalMember<StoryFlag> flags = new("flags");

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
            Id = id.GetValue(),
            SeasonId = season.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Timeline = timeline.GetValue(),
            Level = level.GetValue(),
            Races = races.GetValues(missingMemberBehavior),
            Order = order.GetValue(),
            Chapters = chapters.SelectMany(value => value.GetChapter(missingMemberBehavior)),
            Flags = flags.GetValues(missingMemberBehavior) ?? Array.Empty<StoryFlag>()
        };
    }
}
