using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class GizmoJson
{
    public static Gizmo GetGizmo(this in JsonElement json)
    {
        if (json.TryGetProperty("details", out JsonElement discriminator) && discriminator.TryGetProperty("type", out JsonElement subtype))
        {
            switch (subtype.GetString())
            {
                case "ContainerKey":
                    return json.GetBlackLionChestKey();
                case "RentableContractNpc":
                    return json.GetRentableContractNpc();
                case "UnlimitedConsumable":
                    return json.GetUnlimitedConsumable();
                default:
                    break;
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember level = "level";
        RequiredMember rarity = "rarity";
        RequiredMember vendorValue = "vendor_value";
        RequiredMember gameTypes = "game_types";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        OptionalMember icon = "icon";
        NullableMember guildUpgradeId = "guild_upgrade_id";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Gizmo"))
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
            else if (level.Match(member))
            {
                level = member;
            }
            else if (rarity.Match(member))
            {
                rarity = member;
            }
            else if (vendorValue.Match(member))
            {
                vendorValue = member;
            }
            else if (gameTypes.Match(member))
            {
                gameTypes = member;
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
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (JsonProperty detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error
                            && !detail.Value.ValueEquals("Default"))
                        {
                            ThrowHelper.ThrowInvalidDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (guildUpgradeId.Match(detail))
                    {
                        guildUpgradeId = detail;
                    }
                    else if (detail.NameEquals("vendor_ids"))
                    {
                        // Ignored since /v2/vendors is not active
                        // and I noticed that the data is not consistent
                        // some items that open a vendor panel are missing this field (e.g. Collector's Edition Sandstorm)
                        // some items with multiple vendor tabs are missing vendor ids for one or more tabs (e.g. Ley-Energy Matter Converter)
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

        string? iconString = icon.Map(static (in value) => value.GetString());
        return new Gizmo
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            Level = level.Map(static (in value) => value.GetInt32()),
            Rarity = rarity.Map(static (in value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in value) => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static (in values) => values.GetItemFlags()),
            Restrictions = restrictions.Map(static (in value) => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            GuildUpgradeId = guildUpgradeId.Map(static (in value) => value.GetInt32())
        };
    }
}
