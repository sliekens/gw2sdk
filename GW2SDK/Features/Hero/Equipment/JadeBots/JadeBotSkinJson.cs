using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.JadeBots;

internal static class JadeBotSkinJson
{
    public static JadeBotSkin GetJadeBotSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember unlockItem = "unlock_item";

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
            else if (description.Match(member))
            {
                description = member;
            }
            else if (unlockItem.Match(member))
            {
                unlockItem = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new JadeBotSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            UnlockItemId = unlockItem.Map(value => value.GetInt32())
        };
    }
}
