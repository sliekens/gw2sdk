using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Emotes;

[PublicAPI]
public static class EmoteJson
{
    public static Emote GetEmote(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> commands = new("commands");
        RequiredMember<int> unlockItems = new("unlock_items");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(commands.Name))
            {
                commands.Value = member.Value;
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emote
        {
            Id = id.GetValue(),
            Commands = commands.SelectMany(entry => entry.GetStringRequired()),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32())
        };
    }
}
