﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Files;

[PublicAPI]
public static class AssetJson
{
    public static Asset GetAsset(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> icon = new("icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Asset
        {
            Id = id.GetValue(),
            Icon = icon.GetValue()
        };
    }
}
