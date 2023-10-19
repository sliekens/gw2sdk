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
        RequiredMember id = new("id");
        RequiredMember title = new("title");
        RequiredMember description = new("description");
        RequiredMember journal = new("journal");
        RequiredMember question = new("question");
        OptionalMember professions = new("professions");
        OptionalMember races = new("races");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(title.Name))
            {
                title.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(journal.Name))
            {
                journal.Value = member.Value;
            }
            else if (member.NameEquals(question.Name))
            {
                question.Value = member.Value;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions.Value = member.Value;
            }
            else if (member.NameEquals(races.Name))
            {
                races.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackstoryAnswer
        {
            Id = id.Select(value => value.GetStringRequired()),
            Title = title.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Journal = journal.Select(value => value.GetStringRequired()),
            Question = question.Select(value => value.GetInt32()),
            Professions = professions.SelectMany(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Races = races.SelectMany(value => value.GetEnum<RaceName>(missingMemberBehavior))
        };
    }
}
