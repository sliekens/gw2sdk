﻿using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skins.Json
{
    [PublicAPI]
    public static class SkinReader
    {
        private static ArmorSkin ReadArmorSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Boots":
                    return ReadBootsSkin(json, missingMemberBehavior);
                case "Coat":
                    return ReadCoatSkin(json, missingMemberBehavior);
                case "Gloves":
                    return ReadGlovesSkin(json, missingMemberBehavior);
                case "Helm":
                    return ReadHelmSkin(json, missingMemberBehavior);
                case "HelmAquatic":
                    return ReadHelmAquaticSkin(json, missingMemberBehavior);
                case "Leggings":
                    return ReadLeggingsSkin(json, missingMemberBehavior);
                case "Shoulders":
                    return ReadShouldersSkin(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static AxeSkin ReadAxeSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static BackpackSkin ReadBackpackSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static BootsSkin ReadBootsSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static CoatSkin ReadCoatSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static DaggerSkin ReadDaggerSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static FocusSkin ReadFocusSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ForagingToolSkin ReadForagingToolSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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

            return new ForagingToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static GatheringToolSkin ReadGatheringToolSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Foraging":
                    return ReadForagingToolSkin(json, missingMemberBehavior);
                case "Logging":
                    return ReadLoggingToolSkin(json, missingMemberBehavior);
                case "Mining":
                    return ReadMiningToolSkin(json, missingMemberBehavior);
                case "Foo": // TODO: use real type
                    return ReadFishingToolSkin(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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

            return new GatheringToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static FishingToolSkin ReadFishingToolSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                            if (!detail.Value.ValueEquals("Foo")) // TODO: use real type
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

            return new FishingToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static GlovesSkin ReadGlovesSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static GreatswordSkin ReadGreatswordSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static HammerSkin ReadHammerSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static HarpoonGunSkin ReadHarpoonGunSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static HelmAquaticSkin ReadHelmAquaticSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static HelmSkin ReadHelmSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static LargeBundleSkin ReadLargeBundleSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static LeggingsSkin ReadLeggingsSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        private static LoggingToolSkin ReadLoggingToolSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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

            return new LoggingToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static LongbowSkin ReadLongbowSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static MaceSkin ReadMaceSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static MiningToolSkin ReadMiningToolSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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

            return new MiningToolSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static PistolSkin ReadPistolSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static RifleSkin ReadRifleSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ScepterSkin ReadScepterSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ShieldSkin ReadShieldSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ShortbowSkin ReadShortbowSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ShouldersSkin ReadShouldersSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                WeightClass = weightClass.GetValue(missingMemberBehavior),
                DyeSlots = dyeSlots.Select(value => ReadDyeSlots(value, missingMemberBehavior))
            };
        }

        public static Skin Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type")
                        .GetString())
            {
                case "Armor":
                    return ReadArmorSkin(json, missingMemberBehavior);
                case "Back":
                    return ReadBackpackSkin(json, missingMemberBehavior);
                case "Gathering":
                    return ReadGatheringToolSkin(json, missingMemberBehavior);
                case "Weapon":
                    return ReadWeaponSkin(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull()
            };
        }

        private static SmallBundleSkin ReadSmallBundleSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static SpearSkin ReadSpearSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static StaffSkin ReadStaffSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static SwordSkin ReadSwordSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static TorchSkin ReadTorchSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ToySkin ReadToySkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static ToyTwoHandedSkin ReadToyTwoHandedSkin(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static TridentSkin ReadTridentSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static WarhornSkin ReadWarhornSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.InvalidDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static WeaponSkin ReadWeaponSkin(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("details")
                        .GetProperty("type")
                        .GetString())
            {
                case "Axe":
                    return ReadAxeSkin(json, missingMemberBehavior);
                case "Dagger":
                    return ReadDaggerSkin(json, missingMemberBehavior);
                case "Focus":
                    return ReadFocusSkin(json, missingMemberBehavior);
                case "Greatsword":
                    return ReadGreatswordSkin(json, missingMemberBehavior);
                case "Hammer":
                    return ReadHammerSkin(json, missingMemberBehavior);
                case "LargeBundle":
                    return ReadLargeBundleSkin(json, missingMemberBehavior);
                case "Longbow":
                    return ReadLongbowSkin(json, missingMemberBehavior);
                case "Mace":
                    return ReadMaceSkin(json, missingMemberBehavior);
                case "Pistol":
                    return ReadPistolSkin(json, missingMemberBehavior);
                case "Rifle":
                    return ReadRifleSkin(json, missingMemberBehavior);
                case "Scepter":
                    return ReadScepterSkin(json, missingMemberBehavior);
                case "Shield":
                    return ReadShieldSkin(json, missingMemberBehavior);
                case "Shortbow":
                    return ReadShortbowSkin(json, missingMemberBehavior);
                case "SmallBundle":
                    return ReadSmallBundleSkin(json, missingMemberBehavior);
                case "Spear":
                    return ReadSpearSkin(json, missingMemberBehavior);
                case "Speargun":
                    return ReadHarpoonGunSkin(json, missingMemberBehavior);
                case "Staff":
                    return ReadStaffSkin(json, missingMemberBehavior);
                case "Sword":
                    return ReadSwordSkin(json, missingMemberBehavior);
                case "Torch":
                    return ReadTorchSkin(json, missingMemberBehavior);
                case "Toy":
                    return ReadToySkin(json, missingMemberBehavior);
                case "ToyTwoHanded":
                    return ReadToyTwoHandedSkin(json, missingMemberBehavior);
                case "Trident":
                    return ReadTridentSkin(json, missingMemberBehavior);
                case "Warhorn":
                    return ReadWarhornSkin(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var rarity = new RequiredMember<Rarity>("rarity");
            var flags = new RequiredMember<SkinFlag>("flags");
            var restrictions = new RequiredMember<SkinRestriction>("restrictions");
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
                                throw new InvalidOperationException(
                                    Strings.UnexpectedDiscriminator(detail.Value.GetString()));
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
                Rarity = rarity.GetValue(missingMemberBehavior),
                Flags = flags.GetValues(missingMemberBehavior),
                Restrictions = restrictions.GetValues(missingMemberBehavior),
                Icon = icon.GetValueOrNull(),
                DamageType = damageType.GetValue(missingMemberBehavior)
            };
        }

        private static DyeSlotInfo ReadDyeSlots(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var @default = new RequiredMember<DyeSlot?>("default");
            var asuraFemale = new OptionalMember<DyeSlot?>("AsuraFemale");
            var asuraMale = new OptionalMember<DyeSlot?>("AsuraMale");
            var charrFemale = new OptionalMember<DyeSlot?>("CharrFemale");
            var charrMale = new OptionalMember<DyeSlot?>("CharrMale");
            var humanFemale = new OptionalMember<DyeSlot?>("HumanFemale");
            var humanMale = new OptionalMember<DyeSlot?>("HumanMale");
            var nornFemale = new OptionalMember<DyeSlot?>("NornFemale");
            var nornMale = new OptionalMember<DyeSlot?>("NornMale");
            var sylvariFemale = new OptionalMember<DyeSlot?>("SylvariFemale");
            var sylvariMale = new OptionalMember<DyeSlot?>("SylvariMale");
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
                Default =
                    @default.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                AsuraFemale =
                    asuraFemale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                AsuraMale =
                    asuraMale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                CharrFemale =
                    charrFemale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                CharrMale =
                    charrMale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                HumanFemale =
                    humanFemale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                HumanMale =
                    humanMale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                NornFemale =
                    nornFemale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                NornMale =
                    nornMale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                SylvariFemale =
                    sylvariFemale.SelectMany(value =>
                        value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior)),
                SylvariMale = sylvariMale.SelectMany(value =>
                    value.ValueKind == JsonValueKind.Null ? null : ReadDyeSlot(value, missingMemberBehavior))
            };
        }

        private static DyeSlot ReadDyeSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                Material = material.GetValue(missingMemberBehavior)
            };
        }
    }
}