using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionSkinJson
{
    public static MistChampionSkin GetMistChampionSkin(
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == @default.Name)
            {
                @default = member;
            }
            else if (member.Name == unlockItems.Name)
            {
                unlockItems = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MistChampionSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Default = @default.Map(value => value.GetBoolean()),
            UnlockItemIds = unlockItems.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
