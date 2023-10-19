using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Novelties;

[PublicAPI]
public static class NoveltyJson
{
    public static Novelty GetNovelty(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember icon = new("icon");
        RequiredMember slot = new("slot");
        RequiredMember unlockItems = new("unlock_item");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
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

        return new Novelty
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetStringRequired()),
            Slot = slot.Select(value => value.GetEnum<NoveltyKind>(missingMemberBehavior)),
            UnlockItems = unlockItems.SelectMany(value => value.GetInt32())
        };
    }
}
