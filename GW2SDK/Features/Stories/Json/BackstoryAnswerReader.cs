using System;
using System.Text.Json;

using GW2SDK.Json;
using GW2SDK.Stories.Models;

using JetBrains.Annotations;

namespace GW2SDK.Stories.Json;

[PublicAPI]
public static class BackstoryAnswerReader
{
    public static BackstoryAnswer Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> title = new("title");
        RequiredMember<string> description = new("description");
        RequiredMember<string> journal = new("journal");
        RequiredMember<int> question = new("question");
        OptionalMember<ProfessionName> professions = new("professions");
        OptionalMember<Race> races = new("races");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(title.Name))
            {
                title = title.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(journal.Name))
            {
                journal = journal.From(member.Value);
            }
            else if (member.NameEquals(question.Name))
            {
                question = question.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(races.Name))
            {
                races = races.From(member.Value);
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