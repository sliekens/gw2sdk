using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Items.Json
{
    [PublicAPI]
    public static class ItemReader
    {
        public static Item Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type")
                        .GetString())
            {
                case "Armor":
                    return ReadArmor(json, missingMemberBehavior);
                case "Back":
                    return ReadBackpack(json, missingMemberBehavior);
                case "Bag":
                    return ReadBag(json, missingMemberBehavior);
                case "Consumable":
                    return ReadConsumable(json, missingMemberBehavior);
                case "Container":
                    return ReadContainer(json, missingMemberBehavior);
                case "CraftingMaterial":
                    return ReadCraftingMaterial(json, missingMemberBehavior);
                case "Gathering":
                    return ReadGatheringTool(json, missingMemberBehavior);
                case "Gizmo":
                    return ReadGizmo(json, missingMemberBehavior);
                case "Key":
                    return ReadKey(json, missingMemberBehavior);
                case "MiniPet":
                    return ReadMinipet(json, missingMemberBehavior);
                case "Tool":
                    return ReadTool(json, missingMemberBehavior);
                case "Trinket":
                    return ReadTrinket(json, missingMemberBehavior);
                case "Trophy":
                    return ReadTrophy(json, missingMemberBehavior);
                case "UpgradeComponent":
                    return ReadUpgradeComponent(json, missingMemberBehavior);
                case "Weapon":
                    return ReadWeapon(json, missingMemberBehavior);

                // TODO: use real type 
                case "Qux":
                    return ReadPowerCore(json, missingMemberBehavior);

                // TODO: use real type 
                case "Quux":
                    return ReadJadeBotUpgrade(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Item
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ItemUpgrade ReadItemUpgrade(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var upgrade = new RequiredMember<UpgradeType>("upgrade");
            var itemId = new RequiredMember<int>("item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(upgrade.Name))
                {
                    upgrade = upgrade.From(member.Value);
                }
                else if (member.NameEquals(itemId.Name))
                {
                    itemId = itemId.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemUpgrade
            {
                Upgrade = upgrade.GetValue(missingMemberBehavior),
                ItemId = itemId.GetValue()
            };
        }

        private static InfusionSlot ReadInfusionSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var flags = new RequiredMember<InfusionSlotFlag>("flags");
            var itemId = new NullableMember<int>("item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(itemId.Name))
                {
                    itemId = itemId.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new InfusionSlot
            {
                Flags = flags.GetValues(missingMemberBehavior),
                ItemId = itemId.GetValue()
            };
        }

        private static InfixUpgrade ReadInfixUpgrade(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var attributes = new RequiredMember<UpgradeAttribute>("attributes");
            var buff = new OptionalMember<Buff>("buff");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(attributes.Name))
                {
                    attributes = attributes.From(member.Value);
                }
                else if (member.NameEquals(buff.Name))
                {
                    buff = buff.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new InfixUpgrade
            {
                ItemstatsId = id.GetValue(),
                Attributes = attributes.SelectMany(value => ReadUpgradeAttribute(value, missingMemberBehavior)),
                Buff = buff.Select(value => ReadBuff(value, missingMemberBehavior))
            };
        }

        private static Buff ReadBuff(JsonElement value, MissingMemberBehavior missingMemberBehavior)
        {
            var skillId = new RequiredMember<int>("skill_id");
            var description = new OptionalMember<string>("description");
            foreach (var member in value.EnumerateObject())
            {
                if (member.NameEquals(skillId.Name))
                {
                    skillId = skillId.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Buff
            {
                SkillId = skillId.GetValue(),
                Description = description.GetValueOrEmpty()
            };
        }

        private static UpgradeAttribute ReadUpgradeAttribute(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var attribute = new RequiredMember<UpgradeAttributeName>("attribute");
            var modifier = new RequiredMember<int>("modifier");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(attribute.Name))
                {
                    attribute = attribute.From(member.Value);
                }
                else if (member.NameEquals(modifier.Name))
                {
                    modifier = modifier.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new UpgradeAttribute
            {
                Attribute = attribute.GetValue(missingMemberBehavior),
                Modifier = modifier.GetValue()
            };
        }

        private static Armor ReadArmor(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Boots":
                    return ReadBoots(json, missingMemberBehavior);
                case "Coat":
                    return ReadCoat(json, missingMemberBehavior);
                case "Gloves":
                    return ReadGloves(json, missingMemberBehavior);
                case "Helm":
                    return ReadHelm(json, missingMemberBehavior);
                case "HelmAquatic":
                    return ReadHelmAquatic(json, missingMemberBehavior);
                case "Leggings":
                    return ReadLeggings(json, missingMemberBehavior);
                case "Shoulders":
                    return ReadShoulders(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Armor
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Boots ReadBoots(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Boots"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Boots
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Coat ReadCoat(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Coat"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Coat
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Gloves ReadGloves(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Gloves"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Gloves
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Helm ReadHelm(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Helm"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Helm
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static HelmAquatic ReadHelmAquatic(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("HelmAquatic"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new HelmAquatic
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Leggings ReadLeggings(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Leggings"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Leggings
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Shoulders ReadShoulders(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Shoulders"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Shoulders
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        private static Backpack ReadBackpack(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Back"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (member.NameEquals(upgradesFrom.Name))
                {
                    upgradesFrom = upgradesFrom.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Backpack
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior)),
                UpgradesFrom = upgradesFrom.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static Bag ReadBag(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var noSellOrSort = new RequiredMember<bool>("no_sell_or_sort");
            var size = new RequiredMember<int>("size");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Bag"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals(noSellOrSort.Name))
                        {
                            noSellOrSort = noSellOrSort.From(detail.Value);
                        }
                        else if (detail.NameEquals(size.Name))
                        {
                            size = size.From(detail.Value);
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

            return new Bag
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                NoSellOrSort = noSellOrSort.GetValue(),
                Size = size.GetValue()
            };
        }

        private static Consumable ReadConsumable(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "AppearanceChange":
                    return ReadAppearanceChanger(json, missingMemberBehavior);
                case "Booze":
                    return ReadBooze(json, missingMemberBehavior);
                case "ContractNpc":
                    return ReadContractNpc(json, missingMemberBehavior);
                case "Currency":
                    return ReadCurrency(json, missingMemberBehavior);
                case "Food":
                    return ReadFood(json, missingMemberBehavior);
                case "Generic":
                    return ReadGenericConsumable(json, missingMemberBehavior);
                case "Halloween":
                    return ReadHalloweenConsumable(json, missingMemberBehavior);
                case "Immediate":
                    return ReadImmediateConsumable(json, missingMemberBehavior);
                case "MountRandomUnlock":
                    return ReadMountRandomUnlocker(json, missingMemberBehavior);
                case "RandomUnlock":
                    return ReadRandomUnlocker(json, missingMemberBehavior);
                case "TeleportToFriend":
                    return ReadTeleportToFriend(json, missingMemberBehavior);
                case "Transmutation":
                    return ReadTransmutation(json, missingMemberBehavior);
                case "Unlock":
                    return ReadUnlocker(json, missingMemberBehavior);
                case "UpgradeRemoval":
                    return ReadUpgradeRemover(json, missingMemberBehavior);
                case "Utility":
                    return ReadUtility(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new Consumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static AppearanceChanger ReadAppearanceChanger(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("AppearanceChange"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new AppearanceChanger
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Booze ReadBooze(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Booze"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new Booze
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ContractNpc ReadContractNpc(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("ContractNpc"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ContractNpc
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Currency ReadCurrency(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Currency"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new Currency
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Food ReadFood(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration_ms");
            var applyCount = new NullableMember<int>("apply_count");
            var effectName = new OptionalMember<string>("name");
            var effectIcon = new OptionalMember<string>("icon");
            var effectDescription = new OptionalMember<string>("description");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Food"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(duration.Name))
                        {
                            duration = duration.From(detail.Value);
                        }
                        else if (detail.NameEquals(applyCount.Name))
                        {
                            applyCount = applyCount.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectName.Name))
                        {
                            effectName = effectName.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectIcon.Name))
                        {
                            effectIcon = effectIcon.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectDescription.Name))
                        {
                            effectDescription = effectDescription.From(detail.Value);
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

            return new Food
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty()
            };
        }

        private static GenericConsumable ReadGenericConsumable(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration_ms");
            var applyCount = new NullableMember<int>("apply_count");
            var effectName = new OptionalMember<string>("name");
            var effectIcon = new OptionalMember<string>("icon");
            var effectDescription = new OptionalMember<string>("description");
            var guildUpgradeId = new NullableMember<int>("guild_upgrade_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Generic"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(duration.Name))
                        {
                            duration = duration.From(detail.Value);
                        }
                        else if (detail.NameEquals(applyCount.Name))
                        {
                            applyCount = applyCount.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectName.Name))
                        {
                            effectName = effectName.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectIcon.Name))
                        {
                            effectIcon = effectIcon.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectDescription.Name))
                        {
                            effectDescription = effectDescription.From(detail.Value);
                        }
                        else if (detail.NameEquals(guildUpgradeId.Name))
                        {
                            guildUpgradeId = guildUpgradeId.From(detail.Value);
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

            return new GenericConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty(),
                GuildUpgradeId = guildUpgradeId.GetValue()
            };
        }

        private static HalloweenConsumable ReadHalloweenConsumable(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Halloween"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new HalloweenConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ImmediateConsumable ReadImmediateConsumable(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration_ms");
            var applyCount = new NullableMember<int>("apply_count");
            var effectName = new OptionalMember<string>("name");
            var effectIcon = new OptionalMember<string>("icon");
            var effectDescription = new OptionalMember<string>("description");
            var guildUpgradeId = new NullableMember<int>("guild_upgrade_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Immediate"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(duration.Name))
                        {
                            duration = duration.From(detail.Value);
                        }
                        else if (detail.NameEquals(applyCount.Name))
                        {
                            applyCount = applyCount.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectName.Name))
                        {
                            effectName = effectName.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectIcon.Name))
                        {
                            effectIcon = effectIcon.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectDescription.Name))
                        {
                            effectDescription = effectDescription.From(detail.Value);
                        }
                        else if (detail.NameEquals(guildUpgradeId.Name))
                        {
                            guildUpgradeId = guildUpgradeId.From(detail.Value);
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

            return new ImmediateConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty(),
                GuildUpgradeId = guildUpgradeId.GetValue()
            };
        }

        private static MountRandomUnlocker ReadMountRandomUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("MountRandomUnlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new MountRandomUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static RandomUnlocker ReadRandomUnlocker(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("RandomUnlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new RandomUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static TeleportToFriend ReadTeleportToFriend(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("TeleportToFriend"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new TeleportToFriend
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Transmutation ReadTransmutation(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var skins = new RequiredMember<int>("skins");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Transmutation"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(skins.Name))
                        {
                            skins = skins.From(detail.Value);
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

            return new Transmutation
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Skins = skins.SelectMany(value => value.GetInt32())
            };
        }

        private static Unlocker ReadUnlocker(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("unlock_type")
                        .GetString())
            {
                case "BagSlot":
                    return ReadBagSlotUnlocker(json, missingMemberBehavior);
                case "BankTab":
                    return ReadBankTabUnlocker(json, missingMemberBehavior);
                case "BuildLibrarySlot":
                    return ReadBuildLibrarySlotUnlocker(json, missingMemberBehavior);
                case "BuildLoadoutTab":
                    return ReadBuildLoadoutTabUnlocker(json, missingMemberBehavior);
                case "Champion":
                    return ReadChampionUnlocker(json, missingMemberBehavior);
                case "CollectibleCapacity":
                    return ReadCollectibleCapacityUnlocker(json, missingMemberBehavior);
                case "Content":
                    return ReadContentUnlocker(json, missingMemberBehavior);
                case "CraftingRecipe":
                    return ReadCraftingRecipeUnlocker(json, missingMemberBehavior);
                case "Dye":
                    return ReadDyeUnlocker(json, missingMemberBehavior);
                case "GearLoadoutTab":
                    return ReadGearLoadoutTabUnlocker(json, missingMemberBehavior);
                case "GliderSkin":
                    return ReadGliderSkinUnlocker(json, missingMemberBehavior);
                case "Minipet":
                    return ReadMinipetUnlocker(json, missingMemberBehavior);
                case "Ms":
                    return ReadMsUnlocker(json, missingMemberBehavior);
                case "Outfit":
                    return ReadOutfitUnlocker(json, missingMemberBehavior);
                case "SharedSlot":
                    return ReadSharedSlotUnlocker(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new Unlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static BagSlotUnlocker ReadBagSlotUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BagSlot"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new BagSlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static BankTabUnlocker ReadBankTabUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BankTab"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new BankTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static BuildLibrarySlotUnlocker ReadBuildLibrarySlotUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BuildLibrarySlot"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new BuildLibrarySlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static BuildLoadoutTabUnlocker ReadBuildLoadoutTabUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BuildLoadoutTab"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new BuildLoadoutTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ChampionUnlocker ReadChampionUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Champion"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ChampionUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static CollectibleCapacityUnlocker ReadCollectibleCapacityUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("CollectibleCapacity"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new CollectibleCapacityUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ContentUnlocker ReadContentUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Content"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ContentUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static CraftingRecipeUnlocker ReadCraftingRecipeUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var recipeId = new RequiredMember<int>("recipe_id");
            var extraRecipeIds = new OptionalMember<int>("extra_recipe_ids");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("CraftingRecipe"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(recipeId.Name))
                        {
                            recipeId = recipeId.From(detail.Value);
                        }
                        else if (detail.NameEquals(extraRecipeIds.Name))
                        {
                            extraRecipeIds = extraRecipeIds.From(detail.Value);
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

            return new CraftingRecipeUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                RecipeId = recipeId.GetValue(),
                ExtraRecipeIds = extraRecipeIds.SelectMany(value => value.GetInt32())
            };
        }

        private static DyeUnlocker ReadDyeUnlocker(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var colorId = new RequiredMember<int>("color_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Dye"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(colorId.Name))
                        {
                            colorId = colorId.From(detail.Value);
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

            return new DyeUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                ColorId = colorId.GetValue()
            };
        }

        private static GearLoadoutTabUnlocker ReadGearLoadoutTabUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("GearLoadoutTab"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new GearLoadoutTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static GliderSkinUnlocker ReadGliderSkinUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("GliderSkin"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new GliderSkinUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static MinipetUnlocker ReadMinipetUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Minipet"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new MinipetUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static MsUnlocker ReadMsUnlocker(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Ms"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new MsUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static OutfitUnlocker ReadOutfitUnlocker(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Outfit"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new OutfitUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static SharedSlotUnlocker ReadSharedSlotUnlocker(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Unlock"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("SharedSlot"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new SharedSlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static UpgradeRemover ReadUpgradeRemover(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("UpgradeRemoval"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new UpgradeRemover
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Utility ReadUtility(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration_ms");
            var applyCount = new NullableMember<int>("apply_count");
            var effectName = new OptionalMember<string>("name");
            var effectIcon = new OptionalMember<string>("icon");
            var effectDescription = new OptionalMember<string>("description");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Utility"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(duration.Name))
                        {
                            duration = duration.From(detail.Value);
                        }
                        else if (detail.NameEquals(applyCount.Name))
                        {
                            applyCount = applyCount.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectName.Name))
                        {
                            effectName = effectName.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectIcon.Name))
                        {
                            effectIcon = effectIcon.From(detail.Value);
                        }
                        else if (detail.NameEquals(effectDescription.Name))
                        {
                            effectDescription = effectDescription.From(detail.Value);
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

            return new Utility
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty()
            };
        }

        private static Container ReadContainer(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Default":
                    return ReadDefaultContainer(json, missingMemberBehavior);
                case "GiftBox":
                    return ReadGiftBox(json, missingMemberBehavior);
                case "Immediate":
                    return ReadImmediateContainer(json, missingMemberBehavior);
                case "OpenUI":
                    return ReadOpenUiContainer(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new Container
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static DefaultContainer ReadDefaultContainer(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Default"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new DefaultContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static GiftBox ReadGiftBox(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("GiftBox"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new GiftBox
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ImmediateContainer ReadImmediateContainer(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Immediate"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ImmediateContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static OpenUiContainer ReadOpenUiContainer(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("OpenUI"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new OpenUiContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static CraftingMaterial ReadCraftingMaterial(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("CraftingMaterial"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new CraftingMaterial
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static GatheringTool ReadGatheringTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Foraging":
                    return ReadForagingTool(json, missingMemberBehavior);
                case "Logging":
                    return ReadLoggingTool(json, missingMemberBehavior);
                case "Mining":
                    return ReadMiningTool(json, missingMemberBehavior);
                case "Foo": // TODO: use real type
                    return ReadFishingTool(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new GatheringTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ForagingTool ReadForagingTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Foraging"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ForagingTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static LoggingTool ReadLoggingTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Logging"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new LoggingTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static MiningTool ReadMiningTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Mining"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new MiningTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static FishingTool ReadFishingTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Foo")) // BUG???
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new FishingTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Gizmo ReadGizmo(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "ContainerKey":
                    return ReadContainerKey(json, missingMemberBehavior);
                case "Default":
                    return ReadDefaultGizmo(json, missingMemberBehavior);
                case "RentableContractNpc":
                    return ReadRentableContractNpc(json, missingMemberBehavior);
                case "UnlimitedConsumable":
                    return ReadUnlimitedConsumable(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new Gizmo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static ContainerKey ReadContainerKey(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("ContainerKey"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ContainerKey
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static DefaultGizmo ReadDefaultGizmo(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var vendorIds = new OptionalMember<int>("vendor_ids");
            var guildUpgradeId = new NullableMember<int>("guild_upgrade_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Default"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(vendorIds.Name))
                        {
                            vendorIds = vendorIds.From(detail.Value);
                        }
                        else if (detail.NameEquals(guildUpgradeId.Name))
                        {
                            guildUpgradeId = guildUpgradeId.From(detail.Value);
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

            return new DefaultGizmo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                VendorIds = vendorIds.SelectMany(value => value.GetInt32()),
                GuildUpgradeId = guildUpgradeId.GetValue()
            };
        }

        private static RentableContractNpc ReadRentableContractNpc(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("RentableContractNpc"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new RentableContractNpc
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static UnlimitedConsumable ReadUnlimitedConsumable(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("UnlimitedConsumable"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new UnlimitedConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Key ReadKey(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Key"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Key
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static Minipet ReadMinipet(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var minipetId = new RequiredMember<int>("minipet_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("MiniPet"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals(minipetId.Name))
                        {
                            minipetId = minipetId.From(detail.Value);
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

            return new Minipet
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                MinipetId = minipetId.GetValue()
            };
        }

        private static Tool ReadTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Salvage":
                    return ReadSalvageTool(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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

            return new Tool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static SalvageTool ReadSalvageTool(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var charges = new RequiredMember<int>("charges");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Tool"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Salvage"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(charges.Name))
                        {
                            charges = charges.From(detail.Value);
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

            return new SalvageTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Charges = charges.GetValue()
            };
        }

        private static Trinket ReadTrinket(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Accessory":
                    return ReadAccessory(json, missingMemberBehavior);
                case "Amulet":
                    return ReadAmulet(json, missingMemberBehavior);
                case "Ring":
                    return ReadRing(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (member.NameEquals(upgradesFrom.Name))
                {
                    upgradesFrom = upgradesFrom.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Trinket
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior)),
                UpgradesFrom = upgradesFrom.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static Accessory ReadAccessory(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (member.NameEquals(upgradesFrom.Name))
                {
                    upgradesFrom = upgradesFrom.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Accessory"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Accessory
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior)),
                UpgradesFrom = upgradesFrom.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static Amulet ReadAmulet(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (member.NameEquals(upgradesFrom.Name))
                {
                    upgradesFrom = upgradesFrom.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Amulet"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Amulet
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior)),
                UpgradesFrom = upgradesFrom.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static Ring ReadRing(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(upgradesInto.Name))
                {
                    upgradesInto = upgradesInto.From(member.Value);
                }
                else if (member.NameEquals(upgradesFrom.Name))
                {
                    upgradesFrom = upgradesFrom.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Ring"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
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

            return new Ring
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior)),
                UpgradesFrom = upgradesFrom.SelectMany(value => ReadItemUpgrade(value, missingMemberBehavior))
            };
        }

        private static Trophy ReadTrophy(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trophy"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Trophy
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static UpgradeComponent ReadUpgradeComponent(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Default":
                    return ReadDefaultUpgradeComponent(json, missingMemberBehavior);
                case "Gem":
                    return ReadGem(json, missingMemberBehavior);
                case "Rune":
                    return ReadRune(json, missingMemberBehavior);
                case "Sigil":
                    return ReadSigil(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(upgradeComponentFlags.Name))
                        {
                            upgradeComponentFlags = upgradeComponentFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionUpgradeFlags.Name))
                        {
                            infusionUpgradeFlags = infusionUpgradeFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffix.Name))
                        {
                            suffix = suffix.From(detail.Value);
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

            return new UpgradeComponent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        private static DefaultUpgradeComponent ReadDefaultUpgradeComponent(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            var bonuses = new OptionalMember<string>("bonuses");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Default"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(upgradeComponentFlags.Name))
                        {
                            upgradeComponentFlags = upgradeComponentFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionUpgradeFlags.Name))
                        {
                            infusionUpgradeFlags = infusionUpgradeFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffix.Name))
                        {
                            suffix = suffix.From(detail.Value);
                        }
                        else if (detail.NameEquals(bonuses.Name))
                        {
                            bonuses = bonuses.From(detail.Value);
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

            return new DefaultUpgradeComponent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue(),
                Bonuses = bonuses.SelectMany(value => value.GetStringRequired())
            };
        }

        private static Gem ReadGem(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Gem"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(upgradeComponentFlags.Name))
                        {
                            upgradeComponentFlags = upgradeComponentFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionUpgradeFlags.Name))
                        {
                            infusionUpgradeFlags = infusionUpgradeFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffix.Name))
                        {
                            suffix = suffix.From(detail.Value);
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

            return new Gem
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        private static Rune ReadRune(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            var bonuses = new OptionalMember<string>("bonuses");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Rune"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(upgradeComponentFlags.Name))
                        {
                            upgradeComponentFlags = upgradeComponentFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionUpgradeFlags.Name))
                        {
                            infusionUpgradeFlags = infusionUpgradeFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffix.Name))
                        {
                            suffix = suffix.From(detail.Value);
                        }
                        else if (detail.NameEquals(bonuses.Name))
                        {
                            bonuses = bonuses.From(detail.Value);
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

            return new Rune
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue(),
                Bonuses = bonuses.SelectMany(value => value.GetStringRequired())
            };
        }

        private static Sigil ReadSigil(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Sigil"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(upgradeComponentFlags.Name))
                        {
                            upgradeComponentFlags = upgradeComponentFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionUpgradeFlags.Name))
                        {
                            infusionUpgradeFlags = infusionUpgradeFlags.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffix.Name))
                        {
                            suffix = suffix.From(detail.Value);
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

            return new Sigil
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        private static Weapon ReadWeapon(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Axe":
                    return ReadAxe(json, missingMemberBehavior);
                case "Dagger":
                    return ReadDagger(json, missingMemberBehavior);
                case "Focus":
                    return ReadFocus(json, missingMemberBehavior);
                case "Greatsword":
                    return ReadGreatsword(json, missingMemberBehavior);
                case "Hammer":
                    return ReadHammer(json, missingMemberBehavior);
                case "Harpoon":
                    return ReadSpear(json, missingMemberBehavior);
                case "LargeBundle":
                    return ReadLargeBundle(json, missingMemberBehavior);
                case "LongBow":
                    return ReadLongbow(json, missingMemberBehavior);
                case "Mace":
                    return ReadMace(json, missingMemberBehavior);
                case "Pistol":
                    return ReadPistol(json, missingMemberBehavior);
                case "Rifle":
                    return ReadRifle(json, missingMemberBehavior);
                case "Scepter":
                    return ReadScepter(json, missingMemberBehavior);
                case "Shield":
                    return ReadShield(json, missingMemberBehavior);
                case "ShortBow":
                    return ReadShortbow(json, missingMemberBehavior);
                case "SmallBundle":
                    return ReadSmallBundle(json, missingMemberBehavior);
                case "Speargun":
                    return ReadHarpoonGun(json, missingMemberBehavior);
                case "Staff":
                    return ReadStaff(json, missingMemberBehavior);
                case "Sword":
                    return ReadSword(json, missingMemberBehavior);
                case "Torch":
                    return ReadTorch(json, missingMemberBehavior);
                case "Toy":
                    return ReadToy(json, missingMemberBehavior);
                case "ToyTwoHanded":
                    return ReadToyTwoHanded(json, missingMemberBehavior);
                case "Trident":
                    return ReadTrident(json, missingMemberBehavior);
                case "Warhorn":
                    return ReadWarhorn(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Weapon
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Axe ReadAxe(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Axe"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Axe
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Dagger ReadDagger(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Dagger"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Dagger
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Focus ReadFocus(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Focus"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Focus
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Greatsword ReadGreatsword(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Greatsword"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Greatsword
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Hammer ReadHammer(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Hammer"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Hammer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Spear ReadSpear(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Harpoon"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Spear
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static LargeBundle ReadLargeBundle(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("LargeBundle"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new LargeBundle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Longbow ReadLongbow(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("LongBow"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Longbow
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Mace ReadMace(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Mace"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Mace
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Pistol ReadPistol(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Pistol"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Pistol
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Rifle ReadRifle(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Rifle"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Rifle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Scepter ReadScepter(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Scepter"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Scepter
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Shield ReadShield(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Shield"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Shield
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Shortbow ReadShortbow(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("ShortBow"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Shortbow
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static SmallBundle ReadSmallBundle(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("SmallBundle"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new SmallBundle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static HarpoonGun ReadHarpoonGun(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Speargun"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new HarpoonGun
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Staff ReadStaff(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Staff"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Staff
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Sword ReadSword(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Sword"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Sword
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Torch ReadTorch(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Torch"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Torch
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Toy ReadToy(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Toy"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Toy
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static ToyTwoHanded ReadToyTwoHanded(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("ToyTwoHanded"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new ToyTwoHanded
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Trident ReadTrident(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Trident"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Trident
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static Warhorn ReadWarhorn(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("details"))
                {
                    foreach (var detail in member.Value.EnumerateObject())
                    {
                        if (detail.NameEquals("type"))
                        {
                            if (!detail.Value.ValueEquals("Warhorn"))
                            {
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
                        }
                        else if (detail.NameEquals(minPower.Name))
                        {
                            minPower = minPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(maxPower.Name))
                        {
                            maxPower = maxPower.From(detail.Value);
                        }
                        else if (detail.NameEquals(defense.Name))
                        {
                            defense = defense.From(detail.Value);
                        }
                        else if (detail.NameEquals(infusionSlots.Name))
                        {
                            infusionSlots = infusionSlots.From(detail.Value);
                        }
                        else if (detail.NameEquals(attributeAdjustment.Name))
                        {
                            attributeAdjustment = attributeAdjustment.From(detail.Value);
                        }
                        else if (detail.NameEquals(statChoices.Name))
                        {
                            statChoices = statChoices.From(detail.Value);
                        }
                        else if (detail.NameEquals(infixUpgrade.Name))
                        {
                            infixUpgrade = infixUpgrade.From(detail.Value);
                        }
                        else if (detail.NameEquals(suffixItemId.Name))
                        {
                            suffixItemId = suffixItemId.From(detail.Value);
                        }
                        else if (detail.NameEquals(secondarySuffixItemId.Name))
                        {
                            secondarySuffixItemId = secondarySuffixItemId.From(detail.Value);
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

            return new Warhorn
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.SelectMany(value => ReadInfusionSlot(value, missingMemberBehavior)),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.SelectMany(value => value.GetInt32()),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        private static PowerCore ReadPowerCore(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Qux")) // TODO: use real type
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new PowerCore
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private static JadeBotUpgrade ReadJadeBotUpgrade(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<Coin>("vendor_value");
            var gameTypes = new RequiredMember<GameType>("game_types");
            var flags = new RequiredMember<ItemFlag>("flags");
            var restrictions = new RequiredMember<ItemRestriction>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Quux")) // TODO: use real type
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
                }
                else if (member.NameEquals(vendorValue.Name))
                {
                    vendorValue = vendorValue.From(member.Value);
                }
                else if (member.NameEquals(gameTypes.Name))
                {
                    gameTypes = gameTypes.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(restrictions.Name))
                {
                    restrictions = restrictions.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new JadeBotUpgrade
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValues(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }
    }
}
