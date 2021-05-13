using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class ItemReader : IItemReader,
        IJsonReader<Armor>,
        IJsonReader<Boots>,
        IJsonReader<Coat>,
        IJsonReader<Gloves>,
        IJsonReader<Helm>,
        IJsonReader<HelmAquatic>,
        IJsonReader<Leggings>,
        IJsonReader<Shoulders>,
        IJsonReader<Backpack>,
        IJsonReader<Bag>,
        IJsonReader<Consumable>,
        IJsonReader<AppearanceChanger>,
        IJsonReader<Booze>,
        IJsonReader<ContractNpc>,
        IJsonReader<Currency>,
        IJsonReader<Food>,
        IJsonReader<GenericConsumable>,
        IJsonReader<HalloweenConsumable>,
        IJsonReader<ImmediateConsumable>,
        IJsonReader<MountRandomUnlocker>,
        IJsonReader<RandomUnlocker>,
        IJsonReader<TeleportToFriend>,
        IJsonReader<Transmutation>,
        IJsonReader<Unlocker>,
        IJsonReader<BagSlotUnlocker>,
        IJsonReader<BankTabUnlocker>,
        IJsonReader<BuildLibrarySlotUnlocker>,
        IJsonReader<BuildLoadoutTabUnlocker>,
        IJsonReader<ChampionUnlocker>,
        IJsonReader<CollectibleCapacityUnlocker>,
        IJsonReader<ContentUnlocker>,
        IJsonReader<CraftingRecipeUnlocker>,
        IJsonReader<DyeUnlocker>,
        IJsonReader<GearLoadoutTabUnlocker>,
        IJsonReader<GliderSkinUnlocker>,
        IJsonReader<MinipetUnlocker>,
        IJsonReader<MsUnlocker>,
        IJsonReader<OutfitUnlocker>,
        IJsonReader<SharedSlotUnlocker>,
        IJsonReader<UpgradeRemover>,
        IJsonReader<Utility>,
        IJsonReader<Container>,
        IJsonReader<DefaultContainer>,
        IJsonReader<GiftBox>,
        IJsonReader<ImmediateContainer>,
        IJsonReader<OpenUiContainer>,
        IJsonReader<CraftingMaterial>,
        IJsonReader<GatheringTool>,
        IJsonReader<ForagingTool>,
        IJsonReader<LoggingTool>,
        IJsonReader<MiningTool>,
        IJsonReader<Gizmo>,
        IJsonReader<ContainerKey>,
        IJsonReader<DefaultGizmo>,
        IJsonReader<RentableContractNpc>,
        IJsonReader<UnlimitedConsumable>,
        IJsonReader<Key>,
        IJsonReader<Minipet>,
        IJsonReader<Tool>,
        IJsonReader<SalvageTool>,
        IJsonReader<Trinket>,
        IJsonReader<Accessory>,
        IJsonReader<Amulet>,
        IJsonReader<Ring>,
        IJsonReader<Trophy>,
        IJsonReader<UpgradeComponent>,
        IJsonReader<DefaultUpgradeComponent>,
        IJsonReader<Gem>,
        IJsonReader<Rune>,
        IJsonReader<Sigil>,
        IJsonReader<Weapon>,
        IJsonReader<Axe>,
        IJsonReader<Dagger>,
        IJsonReader<Focus>,
        IJsonReader<Greatsword>,
        IJsonReader<Hammer>,
        IJsonReader<Spear>,
        IJsonReader<LargeBundle>,
        IJsonReader<Longbow>,
        IJsonReader<Mace>,
        IJsonReader<Pistol>,
        IJsonReader<Rifle>,
        IJsonReader<Scepter>,
        IJsonReader<Shield>,
        IJsonReader<Shortbow>,
        IJsonReader<SmallBundle>,
        IJsonReader<HarpoonGun>,
        IJsonReader<Staff>,
        IJsonReader<Sword>,
        IJsonReader<Torch>,
        IJsonReader<Toy>,
        IJsonReader<ToyTwoHanded>,
        IJsonReader<Trident>,
        IJsonReader<Warhorn>
    {
        public Item Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "Armor":
                    return ((IJsonReader<Armor>) this).Read(json, missingMemberBehavior);
                case "Back":
                    return ((IJsonReader<Backpack>) this).Read(json, missingMemberBehavior);
                case "Bag":
                    return ((IJsonReader<Bag>) this).Read(json, missingMemberBehavior);
                case "Consumable":
                    return ((IJsonReader<Consumable>) this).Read(json, missingMemberBehavior);
                case "Container":
                    return ((IJsonReader<Container>) this).Read(json, missingMemberBehavior);
                case "CraftingMaterial":
                    return ((IJsonReader<CraftingMaterial>) this).Read(json, missingMemberBehavior);
                case "Gathering":
                    return ((IJsonReader<GatheringTool>) this).Read(json, missingMemberBehavior);
                case "Gizmo":
                    return ((IJsonReader<Gizmo>) this).Read(json, missingMemberBehavior);
                case "Key":
                    return ((IJsonReader<Key>) this).Read(json, missingMemberBehavior);
                case "MiniPet":
                    return ((IJsonReader<Minipet>) this).Read(json, missingMemberBehavior);
                case "Tool":
                    return ((IJsonReader<Tool>) this).Read(json, missingMemberBehavior);
                case "Trinket":
                    return ((IJsonReader<Trinket>) this).Read(json, missingMemberBehavior);
                case "Trophy":
                    return ((IJsonReader<Trophy>) this).Read(json, missingMemberBehavior);
                case "UpgradeComponent":
                    return ((IJsonReader<UpgradeComponent>) this).Read(json, missingMemberBehavior);
                case "Weapon":
                    return ((IJsonReader<Weapon>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException($"Unexpected discriminator '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Item
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        private ItemUpgrade ReadItemUpgrade(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ItemUpgrade { Upgrade = upgrade.GetValue(), ItemId = itemId.GetValue() };
        }

        private InfusionSlot ReadInfusionSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var flags = new RequiredMember<InfusionSlotFlag[]>("flags");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new InfusionSlot { Flags = flags.GetValue(), ItemId = itemId.GetValue() };
        }

        private InfixUpgrade ReadInfixUpgrade(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var attributes = new RequiredMember<UpgradeAttribute[]>("attributes");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new InfixUpgrade
            {
                ItemstatsId = id.GetValue(),
                Attributes = attributes.Select(value => ReadUpgradeAttributes(value, missingMemberBehavior)),
                Buff = buff.Select(value => ReadBuff(value, missingMemberBehavior))
            };
        }

        private Buff ReadBuff(JsonElement value, MissingMemberBehavior missingMemberBehavior)
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Buff
            {
                SkillId = skillId.GetValue(),
                Description = description.GetValueOrEmpty()
            };
        }

        private UpgradeAttribute[] ReadUpgradeAttributes(JsonElement json, MissingMemberBehavior missingMemberBehavior) =>
            json.GetArray(item => ReadUpgradeAttribute(item, missingMemberBehavior));

        private UpgradeAttribute ReadUpgradeAttribute(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new UpgradeAttribute { Attribute = attribute.GetValue(), Modifier = modifier.GetValue() };
        }

        Armor IJsonReader<Armor>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Boots":
                    return ((IJsonReader<Boots>) this).Read(json, missingMemberBehavior);
                case "Coat":
                    return ((IJsonReader<Coat>) this).Read(json, missingMemberBehavior);
                case "Gloves":
                    return ((IJsonReader<Gloves>) this).Read(json, missingMemberBehavior);
                case "Helm":
                    return ((IJsonReader<Helm>) this).Read(json, missingMemberBehavior);
                case "HelmAquatic":
                    return ((IJsonReader<HelmAquatic>) this).Read(json, missingMemberBehavior);
                case "Leggings":
                    return ((IJsonReader<Leggings>) this).Read(json, missingMemberBehavior);
                case "Shoulders":
                    return ((IJsonReader<Shoulders>) this).Read(json, missingMemberBehavior);
            }
            
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Armor
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Boots IJsonReader<Boots>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Boots
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };

        }

        Coat IJsonReader<Coat>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Coat
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Gloves IJsonReader<Gloves>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Gloves
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Helm IJsonReader<Helm>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Helm
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        HelmAquatic IJsonReader<HelmAquatic>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new HelmAquatic
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Leggings IJsonReader<Leggings>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Leggings
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Shoulders IJsonReader<Shoulders>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Armor"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Shoulders
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue()
            };
        }

        Backpack IJsonReader<Backpack>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade[]>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Back"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Backpack
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior))),
                UpgradesFrom = upgradesFrom.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        Bag IJsonReader<Bag>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Bag
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                NoSellOrSort = noSellOrSort.GetValue(),
                Size = size.GetValue()
            };
        }

        Consumable IJsonReader<Consumable>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "AppearanceChange":
                    return ((IJsonReader<AppearanceChanger>) this).Read(json, missingMemberBehavior);
                case "Booze":
                    return ((IJsonReader<Booze>) this).Read(json, missingMemberBehavior);
                case "ContractNpc":
                    return ((IJsonReader<ContractNpc>) this).Read(json, missingMemberBehavior);
                case "Currency":
                    return ((IJsonReader<Currency>) this).Read(json, missingMemberBehavior);
                case "Food":
                    return ((IJsonReader<Food>) this).Read(json, missingMemberBehavior);
                case "Generic":
                    return ((IJsonReader<GenericConsumable>) this).Read(json, missingMemberBehavior);
                case "Halloween":
                    return ((IJsonReader<HalloweenConsumable>) this).Read(json, missingMemberBehavior);
                case "Immediate":
                    return ((IJsonReader<ImmediateConsumable>) this).Read(json, missingMemberBehavior);
                case "MountRandomUnlock":
                    return ((IJsonReader<MountRandomUnlocker>) this).Read(json, missingMemberBehavior);
                case "RandomUnlock":
                    return ((IJsonReader<RandomUnlocker>) this).Read(json, missingMemberBehavior);
                case "TeleportToFriend":
                    return ((IJsonReader<TeleportToFriend>) this).Read(json, missingMemberBehavior);
                case "Transmutation":
                    return ((IJsonReader<Transmutation>) this).Read(json, missingMemberBehavior);
                case "Unlock":
                    return ((IJsonReader<Unlocker>) this).Read(json, missingMemberBehavior);
                case "UpgradeRemoval":
                    return ((IJsonReader<UpgradeRemover>) this).Read(json, missingMemberBehavior);
                case "Utility":
                    return ((IJsonReader<Utility>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Consumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        AppearanceChanger IJsonReader<AppearanceChanger>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new AppearanceChanger
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Booze IJsonReader<Booze>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Booze
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ContractNpc IJsonReader<ContractNpc>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ContractNpc
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Currency IJsonReader<Currency>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Currency
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Food IJsonReader<Food>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Food
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty()
            };
        }

        GenericConsumable IJsonReader<GenericConsumable>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new GenericConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
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

        HalloweenConsumable IJsonReader<HalloweenConsumable>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new HalloweenConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ImmediateConsumable IJsonReader<ImmediateConsumable>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ImmediateConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
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

        MountRandomUnlocker IJsonReader<MountRandomUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new MountRandomUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        RandomUnlocker IJsonReader<RandomUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new RandomUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        TeleportToFriend IJsonReader<TeleportToFriend>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new TeleportToFriend
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Transmutation IJsonReader<Transmutation>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var skins = new RequiredMember<int[]>("skins");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals(skins.Name))
                        {
                            skins = skins.From(detail.Value);
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Transmutation
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Skins = skins.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }

        Unlocker IJsonReader<Unlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("unlock_type").GetString())
            {
                case "BagSlot":
                    return ((IJsonReader<BagSlotUnlocker>)this).Read(json, missingMemberBehavior);
                case "BankTab":
                    return ((IJsonReader<BankTabUnlocker>)this).Read(json, missingMemberBehavior);
                case "BuildLibrarySlot":
                    return ((IJsonReader<BuildLibrarySlotUnlocker>)this).Read(json, missingMemberBehavior);
                case "BuildLoadoutTab":
                    return ((IJsonReader<BuildLoadoutTabUnlocker>)this).Read(json, missingMemberBehavior);
                case "Champion":
                    return ((IJsonReader<ChampionUnlocker>)this).Read(json, missingMemberBehavior);
                case "CollectibleCapacity":
                    return ((IJsonReader<CollectibleCapacityUnlocker>)this).Read(json, missingMemberBehavior);
                case "Content":
                    return ((IJsonReader<ContentUnlocker>)this).Read(json, missingMemberBehavior);
                case "CraftingRecipe":
                    return ((IJsonReader<CraftingRecipeUnlocker>)this).Read(json, missingMemberBehavior);
                case "Dye":
                    return ((IJsonReader<DyeUnlocker>)this).Read(json, missingMemberBehavior);
                case "GearLoadoutTab":
                    return ((IJsonReader<GearLoadoutTabUnlocker>)this).Read(json, missingMemberBehavior);
                case "GliderSkin":
                    return ((IJsonReader<GliderSkinUnlocker>)this).Read(json, missingMemberBehavior);
                case "Minipet":
                    return ((IJsonReader<MinipetUnlocker>)this).Read(json, missingMemberBehavior);
                case "Ms":
                    return ((IJsonReader<MsUnlocker>)this).Read(json, missingMemberBehavior);
                case "Outfit":
                    return ((IJsonReader<OutfitUnlocker>)this).Read(json, missingMemberBehavior);
                case "SharedSlot":
                    return ((IJsonReader<SharedSlotUnlocker>)this).Read(json, missingMemberBehavior);
            }
            
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Unlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        BagSlotUnlocker IJsonReader<BagSlotUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BagSlot"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BagSlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        BankTabUnlocker IJsonReader<BankTabUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BankTab"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BankTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        BuildLibrarySlotUnlocker IJsonReader<BuildLibrarySlotUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BuildLibrarySlot"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BuildLibrarySlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        BuildLoadoutTabUnlocker IJsonReader<BuildLoadoutTabUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("BuildLoadoutTab"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BuildLoadoutTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ChampionUnlocker IJsonReader<ChampionUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Champion"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ChampionUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        CollectibleCapacityUnlocker IJsonReader<CollectibleCapacityUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("CollectibleCapacity"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new CollectibleCapacityUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ContentUnlocker IJsonReader<ContentUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Content"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ContentUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        CraftingRecipeUnlocker IJsonReader<CraftingRecipeUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var recipeId = new RequiredMember<int>("recipe_id");
            var extraRecipeIds = new OptionalMember<int[]>("extra_recipe_ids");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("CraftingRecipe"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new CraftingRecipeUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                RecipeId = recipeId.GetValue(),
                ExtraRecipeIds = extraRecipeIds.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }

        DyeUnlocker IJsonReader<DyeUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Dye"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals(colorId.Name))
                        {
                            colorId = colorId.From(detail.Value);
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DyeUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                ColorId = colorId.GetValue()
            };
        }

        GearLoadoutTabUnlocker IJsonReader<GearLoadoutTabUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("GearLoadoutTab"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new GearLoadoutTabUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        GliderSkinUnlocker IJsonReader<GliderSkinUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("GliderSkin"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new GliderSkinUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        MinipetUnlocker IJsonReader<MinipetUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Minipet"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new MinipetUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        MsUnlocker IJsonReader<MsUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Ms"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new MsUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        OutfitUnlocker IJsonReader<OutfitUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("Outfit"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new OutfitUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        SharedSlotUnlocker IJsonReader<SharedSlotUnlocker>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals("unlock_type"))
                        {
                            if (!detail.Value.ValueEquals("SharedSlot"))
                            {
                                throw new InvalidOperationException($"Invalid unlocker type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new SharedSlotUnlocker
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        UpgradeRemover IJsonReader<UpgradeRemover>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new UpgradeRemover
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Utility IJsonReader<Utility>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Utility
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                ApplyCount = applyCount.GetValue(),
                EffectName = effectName.GetValueOrEmpty(),
                EffectIcon = effectIcon.GetValueOrNull(),
                EffectDescription = effectDescription.GetValueOrEmpty()
            };
        }

        Container IJsonReader<Container>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Default":
                    return ((IJsonReader<DefaultContainer>)this).Read(json, missingMemberBehavior);
                case "GiftBox":
                    return ((IJsonReader<GiftBox>)this).Read(json, missingMemberBehavior);
                case "Immediate":
                    return ((IJsonReader<ImmediateContainer>)this).Read(json, missingMemberBehavior);
                case "OpenUI":
                    return ((IJsonReader<OpenUiContainer>)this).Read(json, missingMemberBehavior);
            }
            
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Container
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        DefaultContainer IJsonReader<DefaultContainer>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DefaultContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        GiftBox IJsonReader<GiftBox>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new GiftBox
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ImmediateContainer IJsonReader<ImmediateContainer>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ImmediateContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        OpenUiContainer IJsonReader<OpenUiContainer>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Container"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new OpenUiContainer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        CraftingMaterial IJsonReader<CraftingMaterial>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("CraftingMaterial"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new CraftingMaterial
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        GatheringTool IJsonReader<GatheringTool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Foraging":
                    return ((IJsonReader<ForagingTool>)this).Read(json, missingMemberBehavior);
                case "Logging":
                    return ((IJsonReader<LoggingTool>)this).Read(json, missingMemberBehavior);
                case "Mining":
                    return ((IJsonReader<MiningTool>)this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new GatheringTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ForagingTool IJsonReader<ForagingTool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ForagingTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        LoggingTool IJsonReader<LoggingTool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new LoggingTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        MiningTool IJsonReader<MiningTool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gathering"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new MiningTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Gizmo IJsonReader<Gizmo>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "ContainerKey":
                    return ((IJsonReader<ContainerKey>)this).Read(json, missingMemberBehavior);
                case "Default":
                    return ((IJsonReader<DefaultGizmo>)this).Read(json, missingMemberBehavior);
                case "RentableContractNpc":
                    return ((IJsonReader<RentableContractNpc>)this).Read(json, missingMemberBehavior);
                case "UnlimitedConsumable":
                    return ((IJsonReader<UnlimitedConsumable>)this).Read(json, missingMemberBehavior);
            }
            
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Gizmo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        ContainerKey IJsonReader<ContainerKey>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ContainerKey
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        DefaultGizmo IJsonReader<DefaultGizmo>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var vendorIds = new OptionalMember<int[]>("vendor_ids");
            var guildUpgradeId = new NullableMember<int>("guild_upgrade_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DefaultGizmo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                VendorIds = vendorIds.Select(value => value.GetArray(item => item.GetInt32())),
                GuildUpgradeId = guildUpgradeId.GetValue()
            };
        }

        RentableContractNpc IJsonReader<RentableContractNpc>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new RentableContractNpc
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        UnlimitedConsumable IJsonReader<UnlimitedConsumable>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new UnlimitedConsumable
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Key IJsonReader<Key>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Key"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Key
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        Minipet IJsonReader<Minipet>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Minipet
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                MinipetId = minipetId.GetValue()
            };
        }

        Tool IJsonReader<Tool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Salvage":
                    return ((IJsonReader<SalvageTool>)this).Read(json, missingMemberBehavior);
            }
            
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Tool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        SalvageTool IJsonReader<SalvageTool>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
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
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
                            }
                        }
                        else if (detail.NameEquals(charges.Name))
                        {
                            charges = charges.From(detail.Value);
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new SalvageTool
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                Charges = charges.GetValue()
            };
        }

        Trinket IJsonReader<Trinket>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Accessory":
                    return ((IJsonReader<Accessory>)this).Read(json, missingMemberBehavior);
                case "Amulet":
                    return ((IJsonReader<Amulet>)this).Read(json, missingMemberBehavior);
                case "Ring":
                    return ((IJsonReader<Ring>)this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade[]>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gizmo"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Trinket
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior))),
                UpgradesFrom = upgradesFrom.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        Accessory IJsonReader<Accessory>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade[]>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Accessory
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior))),
                UpgradesFrom = upgradesFrom.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        Amulet IJsonReader<Amulet>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade[]>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Amulet
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior))),
                UpgradesFrom = upgradesFrom.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        Ring IJsonReader<Ring>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var upgradesInto = new OptionalMember<ItemUpgrade[]>("upgrades_into");
            var upgradesFrom = new OptionalMember<ItemUpgrade[]>("upgrades_from");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trinket"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Ring
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                UpgradesInto = upgradesInto.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior))),
                UpgradesFrom = upgradesFrom.Select(value => value.GetArray(item => ReadItemUpgrade(item, missingMemberBehavior)))
            };
        }

        Trophy IJsonReader<Trophy>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trophy"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Trophy
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        UpgradeComponent IJsonReader<UpgradeComponent>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Default":
                    return ((IJsonReader<DefaultUpgradeComponent>)this).Read(json, missingMemberBehavior);
                case "Gem":
                    return ((IJsonReader<Gem>)this).Read(json, missingMemberBehavior);
                case "Rune":
                    return ((IJsonReader<Rune>)this).Read(json, missingMemberBehavior);
                case "Sigil":
                    return ((IJsonReader<Sigil>)this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag[]>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag[]>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new UpgradeComponent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValue(),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValue(),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        DefaultUpgradeComponent IJsonReader<DefaultUpgradeComponent>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag[]>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag[]>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            var bonuses = new OptionalMember<string[]>("bonuses");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DefaultUpgradeComponent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValue(),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValue(),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue(),
                Bonuses = bonuses.Select(value => value.GetArray(item => item.GetStringRequired()))
            };
        }

        Gem IJsonReader<Gem>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag[]>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag[]>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Gem
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags = upgradeComponentFlags.GetValue(),
                InfusionUpgradeFlags = infusionUpgradeFlags.GetValue(),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        Rune IJsonReader<Rune>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag[]>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag[]>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            var bonuses = new OptionalMember<string[]>("bonuses");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Rune
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags =
                    upgradeComponentFlags.GetValue(),
                InfusionUpgradeFlags =
                    infusionUpgradeFlags.GetValue(),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue(),
                Bonuses = bonuses.Select(value => value.GetArray(item => item.GetStringRequired()))
            };
        }

        Sigil IJsonReader<Sigil>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var upgradeComponentFlags = new RequiredMember<UpgradeComponentFlag[]>("flags");
            var infusionUpgradeFlags = new RequiredMember<InfusionSlotFlag[]>("infusion_upgrade_flags");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var infixUpgrade = new RequiredMember<InfixUpgrade>("infix_upgrade");
            var suffix = new RequiredMember<string>("suffix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Sigil
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                UpgradeComponentFlags =
                    upgradeComponentFlags.GetValue(),
                InfusionUpgradeFlags =
                    infusionUpgradeFlags.GetValue(),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                Suffix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixName = suffix.GetValue()
            };
        }

        Weapon IJsonReader<Weapon>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Axe":
                    return ((IJsonReader<Axe>)this).Read(json, missingMemberBehavior);
                case "Dagger":
                    return ((IJsonReader<Dagger>)this).Read(json, missingMemberBehavior);
                case "Focus":
                    return ((IJsonReader<Focus>)this).Read(json, missingMemberBehavior);
                case "Greatsword":
                    return ((IJsonReader<Greatsword>)this).Read(json, missingMemberBehavior);
                case "Hammer":
                    return ((IJsonReader<Hammer>)this).Read(json, missingMemberBehavior);
                case "Harpoon":
                    return ((IJsonReader<Spear>)this).Read(json, missingMemberBehavior);
                case "LargeBundle":
                    return ((IJsonReader<LargeBundle>)this).Read(json, missingMemberBehavior);
                case "LongBow":
                    return ((IJsonReader<Longbow>)this).Read(json, missingMemberBehavior);
                case "Mace":
                    return ((IJsonReader<Mace>)this).Read(json, missingMemberBehavior);
                case "Pistol":
                    return ((IJsonReader<Pistol>)this).Read(json, missingMemberBehavior);
                case "Rifle":
                    return ((IJsonReader<Rifle>)this).Read(json, missingMemberBehavior);
                case "Scepter":
                    return ((IJsonReader<Scepter>)this).Read(json, missingMemberBehavior);
                case "Shield":
                    return ((IJsonReader<Shield>)this).Read(json, missingMemberBehavior);
                case "ShortBow":
                    return ((IJsonReader<Shortbow>)this).Read(json, missingMemberBehavior);
                case "SmallBundle":
                    return ((IJsonReader<SmallBundle>)this).Read(json, missingMemberBehavior);
                case "Speargun":
                    return ((IJsonReader<HarpoonGun>)this).Read(json, missingMemberBehavior);
                case "Staff":
                    return ((IJsonReader<Staff>)this).Read(json, missingMemberBehavior);
                case "Sword":
                    return ((IJsonReader<Sword>)this).Read(json, missingMemberBehavior);
                case "Torch":
                    return ((IJsonReader<Torch>)this).Read(json, missingMemberBehavior);
                case "Toy":
                    return ((IJsonReader<Toy>)this).Read(json, missingMemberBehavior);
                case "ToyTwoHanded":
                    return ((IJsonReader<ToyTwoHanded>)this).Read(json, missingMemberBehavior);
                case "Trident":
                    return ((IJsonReader<Trident>)this).Read(json, missingMemberBehavior);
                case "Warhorn":
                    return ((IJsonReader<Warhorn>)this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Unexpected discriminator '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Weapon
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Axe IJsonReader<Axe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Axe
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Dagger IJsonReader<Dagger>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Dagger
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Focus IJsonReader<Focus>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Focus
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Greatsword IJsonReader<Greatsword>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Greatsword
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Hammer IJsonReader<Hammer>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Hammer
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Spear IJsonReader<Spear>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Spear
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        LargeBundle IJsonReader<LargeBundle>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new LargeBundle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Longbow IJsonReader<Longbow>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Longbow
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Mace IJsonReader<Mace>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Mace
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Pistol IJsonReader<Pistol>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Pistol
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Rifle IJsonReader<Rifle>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Rifle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Scepter IJsonReader<Scepter>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Scepter
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Shield IJsonReader<Shield>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Shield
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Shortbow IJsonReader<Shortbow>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Shortbow
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        SmallBundle IJsonReader<SmallBundle>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new SmallBundle
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        HarpoonGun IJsonReader<HarpoonGun>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new HarpoonGun
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Staff IJsonReader<Staff>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Staff
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Sword IJsonReader<Sword>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Sword
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Torch IJsonReader<Torch>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Torch
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Toy IJsonReader<Toy>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Toy
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        ToyTwoHanded IJsonReader<ToyTwoHanded>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ToyTwoHanded
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Trident IJsonReader<Trident>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Trident
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        Warhorn IJsonReader<Warhorn>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var level = new RequiredMember<int>("level");
            var rarity = new RequiredMember<Rarity>("rarity");
            var vendorValue = new RequiredMember<int>("vendor_value");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var gameTypes = new RequiredMember<GameType[]>("game_types");
            var flags = new RequiredMember<ItemFlag[]>("flags");
            var restrictions = new RequiredMember<ItemRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
            var minPower = new RequiredMember<int>("min_power");
            var maxPower = new RequiredMember<int>("max_power");
            var defense = new RequiredMember<int>("defense");
            var infusionSlots = new RequiredMember<InfusionSlot[]>("infusion_slots");
            var attributeAdjustment = new RequiredMember<double>("attribute_adjustment");
            var statChoices = new OptionalMember<int[]>("stat_choices");
            var infixUpgrade = new OptionalMember<InfixUpgrade>("infix_upgrade");
            var suffixItemId = new NullableMember<int>("suffix_item_id");
            var secondarySuffixItemId = new NullableMember<int>("secondary_suffix_item_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                                throw new InvalidOperationException($"Invalid type '{detail.Value.GetString()}'.");
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
                            throw new InvalidOperationException($"Unexpected member '{detail.Name}'.");
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Warhorn
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Level = level.GetValue(),
                Rarity = rarity.GetValue(),
                VendorValue = vendorValue.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                GameTypes = gameTypes.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(),
                MinPower = minPower.GetValue(),
                MaxPower = maxPower.GetValue(),
                Defense = defense.GetValue(),
                InfusionSlots = infusionSlots.Select(value => value.GetArray(item => ReadInfusionSlot(item, missingMemberBehavior))),
                AttributeAdjustment = attributeAdjustment.GetValue(),
                StatChoices = statChoices.Select(value => value.GetArray(item => item.GetInt32())),
                Prefix = infixUpgrade.Select(value => ReadInfixUpgrade(value, missingMemberBehavior)),
                SuffixItemId = suffixItemId.GetValue(),
                SecondarySuffixItemId = secondarySuffixItemId.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
