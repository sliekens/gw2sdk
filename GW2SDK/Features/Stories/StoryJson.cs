﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class StoryJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(season.Name))
            {
                season = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(timeline.Name))
            {
                timeline = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(races.Name))
            {
                races = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(chapters.Name))
            {
                chapters = member;
            }
            else if (member.NameEquals(flags.Name))
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
            SeasonId = season.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Timeline = timeline.Map(value => value.GetStringRequired()),
            Level = level.Map(value => value.GetInt32()),
            Races =
                races.Map(
                    values => values.GetList(
                        value => value.GetEnum<RaceName>(missingMemberBehavior)
                    )
                ),
            Order = order.Map(value => value.GetInt32()),
            Chapters =
                chapters.Map(
                    values => values.GetList(value => value.GetChapter(missingMemberBehavior))
                ),
            Flags = flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<StoryFlag>(missingMemberBehavior)
                    )
                )
                ?? new List<StoryFlag>()
        };
    }
}
