using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class GatheringToolSkinJson
{
    public static GatheringToolSkin GetGatheringToolSkin(this JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator) && discriminator.TryGetProperty("type", out var subtype))
        {
            switch (subtype.GetString())
            {
                case "Fishing":
                    return json.GetFishingToolSkin();
                case "Foraging":
                    return json.GetForagingToolSkin();
                case "Logging":
                    return json.GetLoggingToolSkin();
                case "Mining":
                    return json.GetMiningToolSkin();
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        OptionalMember icon = "icon";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Gathering"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (rarity.Match(member))
            {
                rarity = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (restrictions.Match(member))
            {
                restrictions = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        // Almost certainly a mistake in the API/game so let's not introduce a skin type for bait/lure
                        // https://api.guildwars2.com/v2/skins/10440
                        // [&CsgoAAA=]
                        var discriminatorValue = detail.Value.GetString();
                        if (discriminatorValue is "Bait" or "Lure")
                        {
                            break;
                        }

                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            ThrowHelper.ThrowUnexpectedDiscriminator(discriminatorValue);
                        }
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(detail.Name);
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static value => value.GetString()) ?? "";
        return new GatheringToolSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            Flags = flags.Map(static values => values.GetSkinFlags()),
            Races = restrictions.Map(static values => values.GetRestrictions()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString)
        };
    }
}
