using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class BackstoryQuestionJson
{
    public static BackstoryQuestion GetBackstoryQuestion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember title = new("title");
        RequiredMember description = new("description");
        RequiredMember answers = new("answers");
        RequiredMember order = new("order");
        OptionalMember professions = new("professions");
        OptionalMember races = new("races");
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
            else if (member.NameEquals(answers.Name))
            {
                answers = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
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

        return new BackstoryQuestion
        {
            Id = id.Select(value => value.GetInt32()),
            Title = title.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Answers = answers.SelectMany(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            Professions = professions.SelectMany(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Races = races.SelectMany(value => value.GetEnum<RaceName>(missingMemberBehavior))
        };
    }
}
