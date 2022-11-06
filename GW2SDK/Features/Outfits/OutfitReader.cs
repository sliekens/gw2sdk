using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Outfits;

[PublicAPI]
public static class OutfitReader
{
    public static Outfit GetOutfit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> unlockItems = new("unlock_items");

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
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
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

        return new Outfit
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Icon = icon.GetValue(),
            UnlockItems = unlockItems.SelectMany(value => value.GetInt32())
        };
    }
}
