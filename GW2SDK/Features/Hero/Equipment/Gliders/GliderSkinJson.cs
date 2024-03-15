using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

internal static class GliderSkinJson
{
    public static GliderSkin GetGliderSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GliderSkin
        {
            Id = id.Map(value => value.GetInt32()),
            UnlockItemIds =
                unlockItems.Map(values => values.GetList(entry => entry.GetInt32()))
                ?? Empty.ListOfInt32,
            Order = order.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            DefaultDyeColorIds = defaultDyes.Map(values => values.GetList(entry => entry.GetInt32()))
        };
    }
}
