using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Stories;

[PublicAPI]
public static class BackstoryAnswerJson
{
    public static BackstoryAnswer GetBackstoryAnswer(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> title = new("title");
        RequiredMember<string> description = new("description");
        RequiredMember<string> journal = new("journal");
        RequiredMember<int> question = new("question");
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
            Id = id.GetValue(),
            Title = title.GetValue(),
            Description = description.GetValue(),
            Journal = journal.GetValue(),
            Question = question.GetValue(),
            Professions = professions.GetValues(missingMemberBehavior),
            Races = races.GetValues(missingMemberBehavior)
        };
    }
}
