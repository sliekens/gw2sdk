using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Stories;

[PublicAPI]
public static class BackstoryQuestionJson
{
    public static BackstoryQuestion GetBackstoryQuestion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> title = new("title");
        RequiredMember<string> description = new("description");
        RequiredMember<string> answers = new("answers");
        RequiredMember<int> order = new("order");
        OptionalMember<ProfessionName> professions = new("professions");
        OptionalMember<RaceName> races = new("races");
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
            else if (member.NameEquals(answers.Name))
            {
                answers.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
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

        return new BackstoryQuestion
        {
            Id = id.GetValue(),
            Title = title.GetValue(),
            Description = description.GetValue(),
            Answers = answers.SelectMany(value => value.GetStringRequired()),
            Order = order.GetValue(),
            Professions = professions.GetValues(missingMemberBehavior),
            Races = races.GetValues(missingMemberBehavior)
        };
    }
}
