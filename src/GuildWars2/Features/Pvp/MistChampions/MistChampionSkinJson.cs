using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionSkinJson
{
    public static MistChampionSkin GetMistChampionSkin(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember @default = "default";
        RequiredMember unlockItems = "unlock_items";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (@default.Match(member))
            {
                @default = member;
            }
            else if (unlockItems.Match(member))
            {
                unlockItems = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new MistChampionSkin
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString),
            Default = @default.Map(static (in value) => value.GetBoolean()),
            UnlockItemIds = unlockItems.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
        };
    }
}
