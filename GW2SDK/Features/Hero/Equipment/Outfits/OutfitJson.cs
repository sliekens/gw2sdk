using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Outfits;

internal static class OutfitJson
{
    public static Outfit GetOutfit(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember unlockItems = "unlock_items";

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
            else if (unlockItems.Match(member))
            {
                unlockItems = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Outfit
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            UnlockItemIds =
                unlockItems.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
