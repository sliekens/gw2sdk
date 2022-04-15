﻿using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Quaggans.Models;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Json;

[PublicAPI]
public static class QuagganReader
{
    public static Quaggan Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> url = new("url");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(url.Name))
            {
                url = url.From(member.Value);
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
