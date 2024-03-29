﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SubskillJson
{
    public static Subskill GetSubskill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        NullableMember attunement = "attunement";
        NullableMember form = "form";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (form.Match(member))
            {
                form = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Subskill
        {
            Id = id.Map(value => value.GetInt32()),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Form = form.Map(value => value.GetEnum<Transformation>(missingMemberBehavior))
        };
    }
}
