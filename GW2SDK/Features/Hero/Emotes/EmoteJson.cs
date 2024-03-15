using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Emotes;

internal static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emote
        {
            Id = id.Map(value => value.GetStringRequired()),
            Commands = commands.Map(values => values.GetList(entry => entry.GetStringRequired())),
            UnlockItemIds = unlockItems.Map(values => values.GetList(entry => entry.GetInt32()))
        };
    }
}
