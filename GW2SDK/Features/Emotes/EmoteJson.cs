using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Emotes;

[PublicAPI]
public static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember commands = "commands";
        RequiredMember unlockItems = "unlock_items";

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
            Commands = commands.Select(values => values.GetList(entry => entry.GetStringRequired())),
            UnlockItems = unlockItems.Select(values => values.GetList(entry => entry.GetInt32()))
        };
    }
}
