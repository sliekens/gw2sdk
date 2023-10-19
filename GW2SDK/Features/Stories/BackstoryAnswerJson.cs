using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class BackstoryAnswerJson
{
    public static BackstoryAnswer GetBackstoryAnswer(
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(title.Name))
            {
                title = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(journal.Name))
            {
                journal = member;
            }
            else if (member.NameEquals(question.Name))
            {
                question = member;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = member;
            }
            else if (member.NameEquals(races.Name))
            {
                races = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackstoryAnswer
        {
            Id = id.Map(value => value.GetStringRequired()),
            Title = title.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Journal = journal.Map(value => value.GetStringRequired()),
            Question = question.Map(value => value.GetInt32()),
            Professions = professions.Map(values => values.GetList(value => value.GetEnum<ProfessionName>(missingMemberBehavior))),
            Races = races.Map(values => values.GetList(value => value.GetEnum<RaceName>(missingMemberBehavior)))
        };
    }
}
