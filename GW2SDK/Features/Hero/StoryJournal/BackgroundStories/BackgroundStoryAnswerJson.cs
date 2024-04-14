using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class BackgroundStoryAnswerJson
{
    public static BackgroundStoryAnswer GetBackgroundStoryAnswer(
        this JsonElement json
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackgroundStoryAnswer
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Title = title.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            Journal = journal.Map(static value => value.GetStringRequired()),
            QuestionId = question.Map(static value => value.GetInt32()),
            Professions =
                professions.Map(static values =>
                        values.GetList(static value => value.GetEnum<ProfessionName>()
                        )
                )
                ?? GetValues<ProfessionName>(),
            Races = races.Map(static values => values.GetList(static value => value.GetEnum<RaceName>()
                    )
                )
                ?? GetValues<RaceName>()
        };

        static List<Extensible<TEnum>> GetValues<TEnum>() where TEnum : struct, Enum
        {
#if NET
            return [..Enum.GetValues<TEnum>()];
#else
            return [.. Enum.GetValues(typeof(TEnum)).Cast<TEnum>()];
#endif
        }
    }
}
