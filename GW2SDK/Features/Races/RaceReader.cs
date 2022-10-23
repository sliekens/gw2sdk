using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Races;

[PublicAPI]
public static class RaceReader
{
    public static Race GetRace(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<RaceName> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> skills = new("skills");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Race
        {
            Id = id.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Skills = skills.SelectMany(value => value.GetInt32())
        };
    }
}
