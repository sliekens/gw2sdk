using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroSkinJson
{
    public static HeroSkin GetHeroSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> icon = new("icon");
        RequiredMember<bool> @default = new("default");
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
            else if (member.NameEquals(@default.Name))
            {
                @default.Value = member.Value;
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

        return new HeroSkin
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Icon = icon.GetValue(),
            Default = @default.GetValue(),
            UnlockItemIds = unlockItems.SelectMany(value => value.GetInt32())
        };
    }
}
