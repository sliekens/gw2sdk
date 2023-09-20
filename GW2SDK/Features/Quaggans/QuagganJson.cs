﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quaggans;

[PublicAPI]
public static class QuagganJson
{
    public static Quaggan GetQuaggan(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> url = new("url");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(url.Name))
            {
                url.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Quaggan
        {
            Id = id.GetValue(),
            PictureHref = url.GetValue()
        };
    }
}
