using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroSkinJson
{
    public static HeroSkin GetHeroSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember icon = new("icon");
        RequiredMember @default = new("default");
        RequiredMember unlockItems = new("unlock_items");

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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Default = @default.Select(value => value.GetBoolean()),
            UnlockItemIds = unlockItems.SelectMany(value => value.GetInt32())
        };
    }
}
