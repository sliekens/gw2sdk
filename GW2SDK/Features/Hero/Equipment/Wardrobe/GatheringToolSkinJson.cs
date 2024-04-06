using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class GatheringToolSkinJson
{
    public static GatheringToolSkin GetGatheringToolSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "Fishing":
                        return json.GetFishingToolSkin(missingMemberBehavior);
                    case "Foraging":
                        return json.GetForagingToolSkin(missingMemberBehavior);
                    case "Logging":
                        return json.GetLoggingToolSkin(missingMemberBehavior);
                    case "Mining":
                        return json.GetMiningToolSkin(missingMemberBehavior);
                }
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
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                        // Almost certainly a mistake in the API/game so let's not introduce a skin type for bair/lure
                        // https://api.guildwars2.comv2/skins/10440
                        // [&CsgoAAA=]
                        var discriminatorValue = detail.Value.GetString();
                        if (discriminatorValue is "Bait" or "Lure")
                        {
                            break;
                        }

                        if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(discriminatorValue)
                            );
                        }
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(detail.Name));
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GatheringToolSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Rarity = rarity.Map(value => value.GetEnum<Rarity>()),
            Flags = flags.Map(values => values.GetSkinFlags()),
            Races = restrictions.Map(values => values.GetRestrictions()),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
