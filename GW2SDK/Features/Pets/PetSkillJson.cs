﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pets;

[PublicAPI]
public static class PetSkillJson
{
    public static PetSkill GetPetSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkill { Id = id.Map(value => value.GetInt32()) };
    }
}
