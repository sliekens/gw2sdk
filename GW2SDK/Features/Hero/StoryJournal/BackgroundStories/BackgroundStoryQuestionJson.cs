﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class BackgroundStoryQuestionJson
{
    public static BackgroundStoryQuestion GetBackgroundStoryQuestion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember description = "description";
        RequiredMember answers = "answers";
        RequiredMember order = "order";
        OptionalMember professions = "professions";
        OptionalMember races = "races";
        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (answers.Match(member))
            {
                answers = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (professions.Match(member))
            {
                professions = member;
            }
            else if (races.Match(member))
            {
                races = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackgroundStoryQuestion
        {
            Id = id.Map(value => value.GetInt32()),
            Title = title.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            AnswerIds = answers.Map(values => values.GetList(value => value.GetStringRequired())),
            Order = order.Map(value => value.GetInt32()),
            Professions =
                professions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionName>(missingMemberBehavior)
                        )
                )
                ?? GetValues<ProfessionName>(),
            Races = races.Map(
                    values => values.GetList(
                        value => value.GetEnum<RaceName>(missingMemberBehavior)
                    )
                )
                ?? GetValues<RaceName>()
        };

        static List<TEnum> GetValues<TEnum>() where TEnum : struct, Enum
        {
#if NET
            return [..Enum.GetValues<TEnum>()];
#else
            return [.. Enum.GetValues(typeof(TEnum)).Cast<TEnum>()];
#endif
        }
    }
}
