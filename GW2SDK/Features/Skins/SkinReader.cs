using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public sealed class SkinReader : ISkinReader,
        IJsonReader<ArmorSkin>,
        IJsonReader<BootsSkin>,
        IJsonReader<CoatSkin>,
        IJsonReader<GlovesSkin>,
        IJsonReader<HelmSkin>,
        IJsonReader<HelmAquaticSkin>,
        IJsonReader<LeggingsSkin>,
        IJsonReader<ShouldersSkin>,
        IJsonReader<BackpackSkin>,
        IJsonReader<GatheringToolSkin>,
        IJsonReader<ForagingToolSkin>,
        IJsonReader<LoggingToolSkin>,
        IJsonReader<MiningToolSkin>,
        IJsonReader<WeaponSkin>,
        IJsonReader<AxeSkin>,
        IJsonReader<DaggerSkin>,
        IJsonReader<FocusSkin>,
        IJsonReader<GreatswordSkin>,
        IJsonReader<HammerSkin>,
        IJsonReader<LargeBundleSkin>,
        IJsonReader<LongbowSkin>,
        IJsonReader<MaceSkin>,
        IJsonReader<PistolSkin>,
        IJsonReader<RifleSkin>,
        IJsonReader<ScepterSkin>,
        IJsonReader<ShieldSkin>,
        IJsonReader<ShortbowSkin>,
        IJsonReader<SmallBundleSkin>,
        IJsonReader<SpearSkin>,
        IJsonReader<HarpoonGunSkin>,
        IJsonReader<StaffSkin>,
        IJsonReader<SwordSkin>,
        IJsonReader<TorchSkin>,
        IJsonReader<ToySkin>,
        IJsonReader<ToyTwoHandedSkin>,
        IJsonReader<TridentSkin>,
        IJsonReader<WarhornSkin>
    {
        ArmorSkin IJsonReader<ArmorSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Boots":
                    return ((IJsonReader<BootsSkin>) this).Read(json, missingMemberBehavior);
                case "Coat":
                    return ((IJsonReader<CoatSkin>) this).Read(json, missingMemberBehavior);
                case "Gloves":
                    return ((IJsonReader<GlovesSkin>) this).Read(json, missingMemberBehavior);
                case "Helm":
                    return ((IJsonReader<HelmSkin>) this).Read(json, missingMemberBehavior);
                case "HelmAquatic":
                    return ((IJsonReader<HelmAquaticSkin>) this).Read(json, missingMemberBehavior);
                case "Leggings":
                    return ((IJsonReader<LeggingsSkin>) this).Read(json, missingMemberBehavior);
                case "Shoulders":
                    return ((IJsonReader<ShouldersSkin>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new ArmorSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        AxeSkin IJsonReader<AxeSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new AxeSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        BackpackSkin IJsonReader<BackpackSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new BackpackSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        BootsSkin IJsonReader<BootsSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new BootsSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        CoatSkin IJsonReader<CoatSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new CoatSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        DaggerSkin IJsonReader<DaggerSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new DaggerSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        FocusSkin IJsonReader<FocusSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new FocusSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ForagingToolSkin IJsonReader<ForagingToolSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new ForagingToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        GatheringToolSkin IJsonReader<GatheringToolSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Foraging":
                    return ((IJsonReader<ForagingToolSkin>) this).Read(json, missingMemberBehavior);
                case "Logging":
                    return ((IJsonReader<LoggingToolSkin>) this).Read(json, missingMemberBehavior);
                case "Mining":
                    return ((IJsonReader<MiningToolSkin>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        GlovesSkin IJsonReader<GlovesSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new GlovesSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        GreatswordSkin IJsonReader<GreatswordSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new GreatswordSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        HammerSkin IJsonReader<HammerSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new HammerSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        HarpoonGunSkin IJsonReader<HarpoonGunSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new HarpoonGunSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        HelmAquaticSkin IJsonReader<HelmAquaticSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new HelmAquaticSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        HelmSkin IJsonReader<HelmSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new HelmSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        LargeBundleSkin IJsonReader<LargeBundleSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new LargeBundleSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        LeggingsSkin IJsonReader<LeggingsSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new LeggingsSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        LoggingToolSkin IJsonReader<LoggingToolSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new LoggingToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        LongbowSkin IJsonReader<LongbowSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                            if (!detail.Value.ValueEquals("Longbow"))
                            {
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new LongbowSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        MaceSkin IJsonReader<MaceSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new MaceSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        MiningToolSkin IJsonReader<MiningToolSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
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

            return new MiningToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        PistolSkin IJsonReader<PistolSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new PistolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        RifleSkin IJsonReader<RifleSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new RifleSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ScepterSkin IJsonReader<ScepterSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new ScepterSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ShieldSkin IJsonReader<ShieldSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new ShieldSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ShortbowSkin IJsonReader<ShortbowSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                            if (!detail.Value.ValueEquals("Shortbow"))
                            {
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new ShortbowSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ShouldersSkin IJsonReader<ShouldersSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var weightClass = new RequiredMember<WeightClass>("weight_class");
            var dyeSlots = new OptionalMember<DyeSlotInfo>("dye_slots");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(weightClass.Name))
                        {
                            weightClass = weightClass.From(detail.Value);
                        }
                        else if (detail.NameEquals(dyeSlots.Name))
                        {
                            dyeSlots = dyeSlots.From(detail.Value);
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

            return new ShouldersSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        public Skin Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "Armor":
                    return ((IJsonReader<ArmorSkin>) this).Read(json, missingMemberBehavior);
                case "Back":
                    return ((IJsonReader<BackpackSkin>) this).Read(json, missingMemberBehavior);
                case "Gathering":
                    return ((IJsonReader<GatheringToolSkin>) this).Read(json, missingMemberBehavior);
                case "Weapon":
                    return ((IJsonReader<WeaponSkin>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Skin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull()
            };
        }

        SmallBundleSkin IJsonReader<SmallBundleSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new SmallBundleSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        SpearSkin IJsonReader<SpearSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                            if (!detail.Value.ValueEquals("Spear"))
                            {
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new SpearSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        StaffSkin IJsonReader<StaffSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new StaffSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        SwordSkin IJsonReader<SwordSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new SwordSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        TorchSkin IJsonReader<TorchSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new TorchSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ToySkin IJsonReader<ToySkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new ToySkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        ToyTwoHandedSkin IJsonReader<ToyTwoHandedSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new ToyTwoHandedSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        TridentSkin IJsonReader<TridentSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new TridentSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        WarhornSkin IJsonReader<WarhornSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.InvalidDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new WarhornSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        WeaponSkin IJsonReader<WeaponSkin>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details").GetProperty("type").GetString())
            {
                case "Axe":
                    return ((IJsonReader<AxeSkin>) this).Read(json, missingMemberBehavior);
                case "Dagger":
                    return ((IJsonReader<DaggerSkin>) this).Read(json, missingMemberBehavior);
                case "Focus":
                    return ((IJsonReader<FocusSkin>) this).Read(json, missingMemberBehavior);
                case "Greatsword":
                    return ((IJsonReader<GreatswordSkin>) this).Read(json, missingMemberBehavior);
                case "Hammer":
                    return ((IJsonReader<HammerSkin>) this).Read(json, missingMemberBehavior);
                case "LargeBundle":
                    return ((IJsonReader<LargeBundleSkin>) this).Read(json, missingMemberBehavior);
                case "Longbow":
                    return ((IJsonReader<LongbowSkin>) this).Read(json, missingMemberBehavior);
                case "Mace":
                    return ((IJsonReader<MaceSkin>) this).Read(json, missingMemberBehavior);
                case "Pistol":
                    return ((IJsonReader<PistolSkin>) this).Read(json, missingMemberBehavior);
                case "Rifle":
                    return ((IJsonReader<RifleSkin>) this).Read(json, missingMemberBehavior);
                case "Scepter":
                    return ((IJsonReader<ScepterSkin>) this).Read(json, missingMemberBehavior);
                case "Shield":
                    return ((IJsonReader<ShieldSkin>) this).Read(json, missingMemberBehavior);
                case "Shortbow":
                    return ((IJsonReader<ShortbowSkin>) this).Read(json, missingMemberBehavior);
                case "SmallBundle":
                    return ((IJsonReader<SmallBundleSkin>) this).Read(json, missingMemberBehavior);
                case "Spear":
                    return ((IJsonReader<SpearSkin>) this).Read(json, missingMemberBehavior);
                case "Speargun":
                    return ((IJsonReader<HarpoonGunSkin>) this).Read(json, missingMemberBehavior);
                case "Staff":
                    return ((IJsonReader<StaffSkin>) this).Read(json, missingMemberBehavior);
                case "Sword":
                    return ((IJsonReader<SwordSkin>) this).Read(json, missingMemberBehavior);
                case "Torch":
                    return ((IJsonReader<TorchSkin>) this).Read(json, missingMemberBehavior);
                case "Toy":
                    return ((IJsonReader<ToySkin>) this).Read(json, missingMemberBehavior);
                case "ToyTwoHanded":
                    return ((IJsonReader<ToyTwoHandedSkin>) this).Read(json, missingMemberBehavior);
                case "Trident":
                    return ((IJsonReader<TridentSkin>) this).Read(json, missingMemberBehavior);
                case "Warhorn":
                    return ((IJsonReader<WarhornSkin>) this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag[]>("flags");
            var restrictions = new RequiredMember<SkinRestriction[]>("restrictions");
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var damageType = new RequiredMember<DamageType>("damage_type");
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
                else if (member.NameEquals(rarity.Name))
                {
                    rarity = rarity.From(member.Value);
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
                                throw new InvalidOperationException(Strings.UnexpectedDiscriminator(detail.Value.GetString()));
                            }
                        }
                        else if (detail.NameEquals(damageType.Name))
                        {
                            damageType = damageType.From(detail.Value);
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

            return new WeaponSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(),
                Flags = flags.GetValue(),
                Restrictions = restrictions.GetValue(),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue()
            };
        }

        private DyeSlotInfo ReadDyeSlots(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var @default = new RequiredMember<DyeSlot?[]>("default");
            var asuraFemale = new OptionalMember<DyeSlot?[]>("AsuraFemale");
            var asuraMale = new OptionalMember<DyeSlot?[]>("AsuraMale");
            var charrFemale = new OptionalMember<DyeSlot?[]>("CharrFemale");
            var charrMale = new OptionalMember<DyeSlot?[]>("CharrMale");
            var humanFemale = new OptionalMember<DyeSlot?[]>("HumanFemale");
            var humanMale = new OptionalMember<DyeSlot?[]>("HumanMale");
            var nornFemale = new OptionalMember<DyeSlot?[]>("NornFemale");
            var nornMale = new OptionalMember<DyeSlot?[]>("NornMale");
            var sylvariFemale = new OptionalMember<DyeSlot?[]>("SylvariFemale");
            var sylvariMale = new OptionalMember<DyeSlot?[]>("SylvariMale");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(@default.Name))
                {
                    @default = @default.From(member.Value);
                }
                else if (member.NameEquals("overrides"))
                {
                    foreach (var @override in member.Value.EnumerateObject())
                    {
                        if (@override.NameEquals(asuraFemale.Name))
                        {
                            asuraFemale = asuraFemale.From(@override.Value);
                        }
                        else if (@override.NameEquals(asuraMale.Name))
                        {
                            asuraMale = asuraMale.From(@override.Value);
                        }
                        else if (@override.NameEquals(charrFemale.Name))
                        {
                            charrFemale = charrFemale.From(@override.Value);
                        }
                        else if (@override.NameEquals(charrMale.Name))
                        {
                            charrMale = charrMale.From(@override.Value);
                        }
                        else if (@override.NameEquals(humanFemale.Name))
                        {
                            humanFemale = humanFemale.From(@override.Value);
                        }
                        else if (@override.NameEquals(humanMale.Name))
                        {
                            humanMale = humanMale.From(@override.Value);
                        }
                        else if (@override.NameEquals(nornFemale.Name))
                        {
                            nornFemale = nornFemale.From(@override.Value);
                        }
                        else if (@override.NameEquals(nornMale.Name))
                        {
                            nornMale = nornMale.From(@override.Value);
                        }
                        else if (@override.NameEquals(sylvariFemale.Name))
                        {
                            sylvariFemale = sylvariFemale.From(@override.Value);
                        }
                        else if (@override.NameEquals(sylvariMale.Name))
                        {
                            sylvariMale = sylvariMale.From(@override.Value);
                        }
                        else if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(Strings.UnexpectedMember(@override.Name));
                        }
                    }
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            // The dye slot arrays can contain Null to represent the default color, so this is ugly
            // Perhaps there is a better way to model it with a Null Object pattern?
            return new DyeSlotInfo
            {
                Default = @default.Select(value => value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                AsuraFemale =
                    asuraFemale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                AsuraMale =
                    asuraMale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                CharrFemale =
                    charrFemale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                CharrMale =
                    charrMale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                HumanFemale =
                    humanFemale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                HumanMale =
                    humanMale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                NornFemale =
                    nornFemale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                NornMale =
                    nornMale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                SylvariFemale =
                    sylvariFemale.Select(value =>
                        value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior))),
                SylvariMale = sylvariMale.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(item, missingMemberBehavior)))
            };
        }

        private DyeSlot ReadDyeSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var colorId = new RequiredMember<int>("color_id");
            var material = new RequiredMember<Material>("material");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(colorId.Name))
                {
                    colorId = colorId.From(member.Value);
                }
                else if (member.NameEquals(material.Name))
                {
                    material = material.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new DyeSlot
            {
                ColorId = colorId.GetValue(),
                Material = material.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
