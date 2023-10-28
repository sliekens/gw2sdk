using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Emotes;

internal static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember commands = "commands";
        RequiredMember unlockItems = "unlock_items";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == commands.Name)
            {
                commands = member;
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

        return new Emote
        {
            Id = id.Map(value => value.GetStringRequired()),
            Commands = commands.Map(values => values.GetList(entry => entry.GetStringRequired())),
            UnlockItems = unlockItems.Map(values => values.GetList(entry => entry.GetInt32()))
        };
    }
}
