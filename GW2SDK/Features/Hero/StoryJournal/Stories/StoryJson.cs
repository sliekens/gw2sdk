using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StoryJson
{
    public static Story GetStory(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Story
        {
            Id = id.Map(value => value.GetInt32()),
            StorylineId = season.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Timeline = timeline.Map(value => value.GetStringRequired()),
            Level = level.Map(value => value.GetInt32()),
            Races =
                races.Map(
                    values => values.GetList(
                        value => value.GetEnum<RaceName>(missingMemberBehavior)
                    )
                )
                ?? GetValues<RaceName>(),
            Order = order.Map(value => value.GetInt32()),
            Chapters =
                chapters.Map(
                    values => values.GetList(value => value.GetChapter(missingMemberBehavior))
                ),
            Flags = flags.Map(values => values.GetStoryFlags()) ?? StoryFlags.None
        };

        static List<TEnum> GetValues<TEnum>() where TEnum : struct, Enum
        {
#if NET
            return [.. Enum.GetValues<TEnum>()];
#else
            return [.. Enum.GetValues(typeof(TEnum)).Cast<TEnum>()];
#endif
        }
    }
}
