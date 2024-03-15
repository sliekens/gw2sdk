using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal static class NoveltyJson
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
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
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

        return new Novelty
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            IconHref = icon.Map(value => value.GetStringRequired()),
            Slot = slot.Map(value => value.GetEnum<NoveltyKind>(missingMemberBehavior)),
            UnlockItemIds = unlockItems.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
