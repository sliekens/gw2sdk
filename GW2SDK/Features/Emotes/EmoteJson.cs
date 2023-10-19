using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Emotes;

[PublicAPI]
public static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember commands = new("commands");
        RequiredMember unlockItems = new("unlock_items");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(commands.Name))
            {
                commands = member;
            }
            else if (member.NameEquals(unlockItems.Name))
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
            Id = id.Select(value => value.GetStringRequired()),
            Commands = commands.SelectMany(entry => entry.GetStringRequired()),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32())
        };
    }
}
