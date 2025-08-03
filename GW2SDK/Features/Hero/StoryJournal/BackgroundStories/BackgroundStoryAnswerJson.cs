using System.Text.Json;

using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class BackgroundStoryAnswerJson
{
    public static BackgroundStoryAnswer GetBackgroundStoryAnswer(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember description = "description";
        RequiredMember journal = "journal";
        RequiredMember question = "question";
        OptionalMember professions = "professions";
        OptionalMember races = "races";
        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new BackgroundStoryAnswer
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Title = title.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
            Journal = journal.Map(static (in JsonElement value) => value.GetStringRequired()),
            QuestionId = question.Map(static (in JsonElement value) => value.GetInt32()),
            Professions =
                professions.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<ProfessionName>())
                )
                ?? Profession.AllProfessions,
            Races = races.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<RaceName>())
                )
                ?? Race.AllRaces
        };
    }
}
