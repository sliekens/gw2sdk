﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal static class MountSkinJson
{
    public static MountSkin GetMountSkin(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";
        RequiredMember mount = "mount";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (dyeSlots.Match(member))
            {
                dyeSlots = member;
            }
            else if (mount.Match(member))
            {
                mount = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static value => value.GetStringRequired());
        return new MountSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots =
                dyeSlots.Map(static values => values.GetList(static value => value.GetDyeSlot())),
            Mount = mount.Map(static value => value.GetMountName())
        };
    }
}
