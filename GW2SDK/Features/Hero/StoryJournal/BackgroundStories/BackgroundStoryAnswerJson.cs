using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class BackgroundStoryAnswerJson
{
    public static BackgroundStoryAnswer GetBackgroundStoryAnswer(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember description = "description";
        RequiredMember journal = "journal";
        RequiredMember question = "question";
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
            else if (journal.Match(member))
            {
                journal = member;
            }
            else if (question.Match(member))
            {
                question = member;
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

        return new BackgroundStoryAnswer
        {
            Id = id.Map(value => value.GetStringRequired()),
            Title = title.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Journal = journal.Map(value => value.GetStringRequired()),
            QuestionId = question.Map(value => value.GetInt32()),
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
