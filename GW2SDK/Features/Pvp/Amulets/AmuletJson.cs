using System;
using System.Text.Json;
using GuildWars2.Hero.Equipment;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Amulets;

internal static class AmuletJson
{
    public static Amulet GetAmulet(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember attributes = "attributes";

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
            else if (attributes.Match(member))
            {
                attributes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static value => value.GetStringRequired());
        return new Amulet
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString),
            Attributes = attributes.Map(static value => value.GetAttributes())
        };
    }
}
