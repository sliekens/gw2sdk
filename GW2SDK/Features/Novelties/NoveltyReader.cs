using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
public static class NoveltyReader
{
    public static Novelty GetNovelty(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<string> icon = new("icon");
        RequiredMember<NoveltyKind> slot = new("slot");
        RequiredMember<int> unlockItems = new("unlock_item");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Icon = icon.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            UnlockItems = unlockItems.SelectMany(value => value.GetInt32())
        };
    }
}
