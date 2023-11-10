using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Backstory;

internal static class BackstoryQuestionJson
{
    public static BackstoryQuestion GetBackstoryQuestion(
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == title.Name)
            {
                title = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == answers.Name)
            {
                answers = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == professions.Name)
            {
                professions = member;
            }
            else if (member.Name == races.Name)
            {
                races = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackstoryQuestion
        {
            Id = id.Map(value => value.GetInt32()),
            Title = title.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Answers = answers.Map(values => values.GetList(value => value.GetStringRequired())),
            Order = order.Map(value => value.GetInt32()),
            Professions =
                professions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionName>(missingMemberBehavior)
                        )
                ),
            Races = races.Map(
                values => values.GetList(value => value.GetEnum<RaceName>(missingMemberBehavior))
            )
        };
    }
}
