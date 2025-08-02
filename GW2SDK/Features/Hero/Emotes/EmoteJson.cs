using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Emotes;

internal static class EmoteJson
{
    public static Emote GetEmote(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember commands = "commands";
        RequiredMember unlockItems = "unlock_items";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (commands.Match(member))
            {
                commands = member;
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

        return new Emote
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Commands =
                commands.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
            UnlockItemIds =
                unlockItems.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
        };
    }
}
