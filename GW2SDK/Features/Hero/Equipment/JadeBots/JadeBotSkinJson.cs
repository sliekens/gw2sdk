using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.JadeBots;

internal static class JadeBotSkinJson
{
    public static JadeBotSkin GetJadeBotSkin(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember unlockItem = "unlock_item";

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
            else if (description.Match(member))
            {
                description = member;
            }
            else if (unlockItem.Match(member))
            {
                unlockItem = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new JadeBotSkin
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
            UnlockItemId = unlockItem.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
