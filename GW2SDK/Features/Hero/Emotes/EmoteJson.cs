using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Emotes;

internal static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json)
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emote
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Commands =
                commands.Map(
                    static values => values.GetList(static value => value.GetStringRequired())
                ),
            UnlockItemIds =
                unlockItems.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
