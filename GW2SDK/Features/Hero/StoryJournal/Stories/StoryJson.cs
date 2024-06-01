using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StoryJson
{
    public static Story GetStory(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember season = "season";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember timeline = "timeline";
        RequiredMember level = "level";
        OptionalMember races = "races";
        RequiredMember order = "order";
        RequiredMember chapters = "chapters";
        OptionalMember flags = "flags";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (season.Match(member))
            {
                season = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (timeline.Match(member))
            {
                timeline = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (races.Match(member))
            {
                races = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (chapters.Match(member))
            {
                chapters = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Story
        {
            Id = id.Map(static value => value.GetInt32()),
            StorylineId = season.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            Timeline = timeline.Map(static value => value.GetStringRequired()),
            Level = level.Map(static value => value.GetInt32()),
            Races =
                races.Map(
                    static values => values.GetList(static value => value.GetEnum<RaceName>())
                )
                ?? GetValues<RaceName>(),
            Order = order.Map(static value => value.GetInt32()),
            Chapters =
                chapters.Map(static values => values.GetList(static value => value.GetChapter())),
            Flags = flags.Map(static values => values.GetStoryFlags()) ?? StoryFlags.None
        };

        static List<Extensible<TEnum>> GetValues<TEnum>() where TEnum : struct, Enum
        {
#if NET
            return [.. Enum.GetValues<TEnum>()];
#else
            return [.. Enum.GetValues(typeof(TEnum)).Cast<TEnum>()];
#endif
        }
    }
}
