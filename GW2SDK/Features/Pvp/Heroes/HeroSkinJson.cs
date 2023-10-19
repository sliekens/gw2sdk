﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroSkinJson
{
    public static HeroSkin GetHeroSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember @default = "default";
        RequiredMember unlockItems = "unlock_items";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(@default.Name))
            {
                @default = member;
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HeroSkin
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Default = @default.Select(value => value.GetBoolean()),
            UnlockItemIds = unlockItems.Select(values => values.GetList(value => value.GetInt32()))
        };
    }
}
