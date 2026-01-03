using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

internal static class GliderSkinJson
{
    public static GliderSkin GetGliderSkin(this in JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember defaultDyes = "default_dyes";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (unlockItems.Match(member))
            {
                unlockItems = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (defaultDyes.Match(member))
            {
                defaultDyes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new GliderSkin
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            UnlockItemIds =
                unlockItems.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
                ?? [],
            Order = order.Map(static (in value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            DefaultDyeColorIds =
                defaultDyes.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
        };
    }
}
