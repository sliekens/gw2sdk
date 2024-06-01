using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

internal static class GliderSkinJson
{
    public static GliderSkin GetGliderSkin(this JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember defaultDyes = "default_dyes";

        foreach (var member in json.EnumerateObject())
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

        return new GliderSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            UnlockItemIds =
                unlockItems.Map(static values => values.GetList(static value => value.GetInt32()))
                ?? Empty.ListOfInt32,
            Order = order.Map(static value => value.GetInt32()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            DefaultDyeColorIds =
                defaultDyes.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
