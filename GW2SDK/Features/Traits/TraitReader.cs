using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitReader : ITraitReader
    {
        public Trait Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var tier = new RequiredMember<int>("tier");
            var order = new RequiredMember<int>("order");
            var name = new RequiredMember<string>("name");
            var description = new OptionalMember<string>("description");
            var slot = new RequiredMember<TraitSlot>("slot");
            var facts = new OptionalMember<TraitFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitCombinationFact[]>("traited_facts");
            var skills = new OptionalMember<TraitSkill[]>("skills");
            var specialization = new RequiredMember<int>("specialization");
            var icon = new RequiredMember<string>("icon");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(tier.Name))
                {
                    tier = tier.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
                }
                else if (member.NameEquals(facts.Name))
                {
                    facts = facts.From(member.Value);
                }
                else if (member.NameEquals(traitedFacts.Name))
                {
                    traitedFacts = traitedFacts.From(member.Value);
                }
                else if (member.NameEquals(skills.Name))
                {
                    skills = skills.From(member.Value);
                }
                else if (member.NameEquals(specialization.Name))
                {
                    specialization = specialization.From(member.Value);
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

            return new Trait
            {
                Id = id.GetValue(),
                Tier = tier.GetValue(),
                Order = order.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValueOrEmpty(),
                Slot = slot.GetValue(),
                Icon = icon.GetValue(),
                SpezializationId = specialization.GetValue(),
                Facts = facts.Select(value => value.GetArray(item => ReadTraitFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts = traitedFacts.Select(value => value.GetArray(item => ReadTraitCombinationFact(item, missingMemberBehavior))),
                Skills = skills.Select(value => value.GetArray(item => ReadTraitSkill(item, missingMemberBehavior)))
            };
        }

        private TraitFact ReadTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;

            // BUG: Life Force Cost is missing a type property but we can treat it as Percent
            if (!json.TryGetProperty("type", out var type) && json.TryGetProperty("percent", out _))
            {
                return ReadPercentTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
            }

            switch (type.GetString())
            {
                case "AttributeAdjust":
                    return ReadAttributeAdjustTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Buff":
                    return ReadBuffTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "BuffConversion":
                    return ReadBuffConversionTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "ComboField":
                    return ReadComboFieldTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "ComboFinisher":
                    return ReadComboFinisherTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Damage":
                    return ReadDamageTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Distance":
                    return ReadDistanceTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "NoData":
                    return ReadNoDataTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Number":
                    return ReadNumberTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Percent":
                    return ReadPercentTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "PrefixedBuff":
                    return ReadPrefixedBuffTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Radius":
                    return ReadRadiusTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Range":
                    return ReadRangeTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Recharge":
                    return ReadRechargeTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "StunBreak":
                    return ReadStunBreakTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Time":
                    return ReadTimeTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Unblockable":
                    return ReadUnblockableTraitFact(json, missingMemberBehavior, out requiresTrait, out overrides);
            }

            var text = new OptionalMember<string>("text");
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
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
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

            return new TraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty()
            };
        }

        private AttributeAdjustTraitFact ReadAttributeAdjustTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<int>("value");
            var target = new RequiredMember<TraitTarget>("target");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("AttributeAdjust"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (member.NameEquals(target.Name))
                {
                    target = target.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new AttributeAdjustTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue(),
                Target = target.GetValue()
            };
        }

        private BuffTraitFact ReadBuffTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
            var applyCount = new NullableMember<int>("apply_count");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Buff"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(duration.Name))
                {
                    duration = duration.From(member.Value);
                }
                else if (member.NameEquals(status.Name))
                {
                    status = status.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(applyCount.Name))
                {
                    applyCount = applyCount.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BuffTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
                Status = status.GetValueOrEmpty(),
                Description = description.GetValueOrEmpty(),
                ApplyCount = applyCount.GetValue()
            };
        }

        private BuffConversionTraitFact ReadBuffConversionTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var percent = new RequiredMember<int>("percent");
            var source = new RequiredMember<TraitTarget>("source");
            var target = new RequiredMember<TraitTarget>("target");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("BuffConversion"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(percent.Name))
                {
                    percent = percent.From(member.Value);
                }
                else if (member.NameEquals(source.Name))
                {
                    source = source.From(member.Value);
                }
                else if (member.NameEquals(target.Name))
                {
                    target = target.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new BuffConversionTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Percent = percent.GetValue(),
                Source = source.GetValue(),
                Target = target.GetValue()
            };
        }

        private ComboFieldTraitFact ReadComboFieldTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var fieldType = new RequiredMember<ComboFieldName>("field_type");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ComboField"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(fieldType.Name))
                {
                    fieldType = fieldType.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ComboFieldTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Field = fieldType.GetValue()
            };
        }

        private ComboFinisherTraitFact ReadComboFinisherTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var percent = new RequiredMember<int>("percent");
            var finisherType = new RequiredMember<ComboFinisherName>("finisher_type");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ComboFinisher"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(percent.Name))
                {
                    percent = percent.From(member.Value);
                }
                else if (member.NameEquals(finisherType.Name))
                {
                    finisherType = finisherType.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ComboFinisherTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Percent = percent.GetValue(),
                FinisherName = finisherType.GetValue()
            };
        }

        private DamageTraitFact ReadDamageTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var hitCount = new RequiredMember<int>("hit_count");
            var damageMultiplier = new RequiredMember<double>("dmg_multiplier");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Damage"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(hitCount.Name))
                {
                    hitCount = hitCount.From(member.Value);
                }
                else if (member.NameEquals(damageMultiplier.Name))
                {
                    damageMultiplier = damageMultiplier.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DamageTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                HitCount = hitCount.GetValue(),
                DamageMultiplier = damageMultiplier.GetValue()
            };
        }

        private DistanceTraitFact ReadDistanceTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var distance = new RequiredMember<int>("distance");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Distance"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(distance.Name))
                {
                    distance = distance.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new DistanceTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Distance = distance.GetValue()
            };
        }

        private NoDataTraitFact ReadNoDataTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("NoData"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
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

            return new NoDataTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty()
            };
        }

        private NumberTraitFact ReadNumberTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<int>("value");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Number"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new NumberTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue()
            };
        }

        private PercentTraitFact ReadPercentTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var percent = new RequiredMember<double>("percent");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Percent"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(percent.Name))
                {
                    percent = percent.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new PercentTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Percent = percent.GetValue()
            };
        }

        private PrefixedBuffTraitFact ReadPrefixedBuffTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
            var applyCount = new NullableMember<int>("apply_count");
            var prefix = new RequiredMember<BuffPrefix>("prefix");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("PrefixedBuff"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(duration.Name))
                {
                    duration = duration.From(member.Value);
                }
                else if (member.NameEquals(status.Name))
                {
                    status = status.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(applyCount.Name))
                {
                    applyCount = applyCount.From(member.Value);
                }
                else if (member.NameEquals(prefix.Name))
                {
                    prefix = prefix.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new PrefixedBuffTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
                Status = status.GetValueOrEmpty(),
                Description = description.GetValueOrEmpty(),
                ApplyCount = applyCount.GetValue(),
                Prefix = prefix.Select(value => ReadBuffPrefix(value, missingMemberBehavior))
            };
        }

        private BuffPrefix ReadBuffPrefix(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(status.Name))
                {
                    status = status.From(member.Value);
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

            return new BuffPrefix
            {
                Text = text.GetValue(),
                Icon = icon.GetValue(),
                Status = status.GetValueOrEmpty(),
                Description = description.GetValueOrEmpty()
            };
        }

        private RadiusTraitFact ReadRadiusTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var distance = new RequiredMember<int>("distance");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Radius"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(distance.Name))
                {
                    distance = distance.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new RadiusTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Distance = distance.GetValue()
            };
        }

        private RangeTraitFact ReadRangeTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<int>("value");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Range"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new RangeTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue()
            };
        }

        private RechargeTraitFact ReadRechargeTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<double>("value");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Recharge"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new RechargeTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue()
            };
        }

        private StunBreakTraitFact ReadStunBreakTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<bool>("value");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("StunBreak"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new StunBreakTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue()
            };
        }

        private TimeTraitFact ReadTimeTraitFact(JsonElement json, MissingMemberBehavior missingMemberBehavior, out int? requiresTrait, out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var duration = new RequiredMember<TimeSpan>("duration");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Time"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(duration.Name))
                {
                    duration = duration.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new TimeTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble()))
            };
        }

        private UnblockableTraitFact ReadUnblockableTraitFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides)
        {
            requiresTrait = null;
            overrides = null;
            var text = new OptionalMember<string>("text");
            var icon = new OptionalMember<string>("icon");
            var value = new RequiredMember<bool>("value");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Unblockable"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
                    }
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new UnblockableTraitFact
            {
                Text = text.GetValueOrEmpty(),
                Icon = icon.GetValueOrEmpty(),
                Value = value.GetValue()
            };
        }

        private TraitCombinationFact ReadTraitCombinationFact(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var fact = ReadTraitFact(json, missingMemberBehavior, out var requiresTrait, out var overrides);
            return new TraitCombinationFact
            {
                Fact = fact,
                RequiresTrait = requiresTrait.GetValueOrDefault(),
                Overrides = overrides
            };
        }

        private TraitSkill ReadTraitSkill(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var facts = new RequiredMember<TraitFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitCombinationFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new RequiredMember<string>("icon");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(facts.Name))
                {
                    facts = facts.From(member.Value);
                }
                else if (member.NameEquals(traitedFacts.Name))
                {
                    traitedFacts = traitedFacts.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals("flags"))
                {
                    // This seems to be always empty, just ignore it until one day it isn't
                    if (missingMemberBehavior == MissingMemberBehavior.Error && member.Value.GetArrayLength() != 0)
                    {
                        throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(categories.Name))
                {
                    categories = categories.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new TraitSkill
            {
                Name = name.GetValue(),
                Facts = facts.Select(value => value.GetArray(item => ReadTraitFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts = traitedFacts.Select(value => value.GetArray(item => ReadTraitCombinationFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValue(),
                Id = id.GetValue(),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
