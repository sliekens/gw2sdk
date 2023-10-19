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
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember slot = "slot";
        RequiredMember unlockItems = "unlock_item";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = member;
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

        return new Novelty
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetStringRequired()),
            Slot = slot.Select(value => value.GetEnum<NoveltyKind>(missingMemberBehavior)),
            UnlockItems = unlockItems.Select(values => values.GetList(value => value.GetInt32()))
        };
    }
}
