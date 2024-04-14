using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal static class NoveltyJson
{
    public static Novelty GetNovelty(
        this JsonElement json
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Novelty
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Slot = slot.Map(static value => value.GetEnum<NoveltyKind>()),
            UnlockItemIds = unlockItems.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
