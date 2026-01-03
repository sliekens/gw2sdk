using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal static class NoveltyJson
{
    public static Novelty GetNovelty(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember slot = "slot";
        RequiredMember unlockItems = "unlock_item";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());

        return new Novelty
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString),
            Slot = slot.Map(static (in value) => value.GetEnum<NoveltyKind>()),
            UnlockItemIds =
                unlockItems.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
        };
    }
}
