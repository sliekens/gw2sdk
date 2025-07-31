using System.Text.Json;
using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class BackgroundStoryQuestionJson
{
    public static BackgroundStoryQuestion GetBackgroundStoryQuestion(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new BackgroundStoryQuestion
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Title = title.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
            AnswerIds =
                answers.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
            Order = order.Map(static (in JsonElement value) => value.GetInt32()),
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
