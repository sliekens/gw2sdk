﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Races;

[PublicAPI]
public static class RaceJson
{
    public static Race GetRace(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember skills = new("skills");

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
            Id = id.Select(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Skills = skills.SelectMany(value => value.GetInt32())
        };
    }
}
