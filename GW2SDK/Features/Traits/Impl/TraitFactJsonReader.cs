using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using JsonValueKind = System.Text.Json.JsonValueKind;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitFactJsonReader : IJsonReader<TraitFact>
    {
        public TraitFact Read(in JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("JSON is not an object.");
            }

            var typeName = nameof(TraitFact);
            var discriminator = json.TryGetProperty("type", out var type) ? type.GetString() : "";
            return discriminator switch
            {
                "AttributeAdjust" => ReadAttributeAdjustTraitFact(json),
                "Buff" => ReadBuffTraitFact(json),
                "BuffConversion" => ReadBuffConversionTraitFact(json),
                "ComboField" => ReadComboFieldTraitFact(json),
                "ComboFinisher" => ReadComboFinisherTraitFact(json),
                "Damage" => ReadDamageTraitFact(json),
                "Distance" => ReadDistanceTraitFact(json),
                "NoData" => ReadNoDataTraitFact(json),
                "Number" => ReadNumberTraitFact(json),
                "Percent" => ReadPercentTraitFact(json),
                "PrefixedBuff" => ReadPrefixedBuffTraitFact(json),
                "Radius" => ReadRadiusTraitFact(json),
                "Range" => ReadRangeTraitFact(json),
                "Recharge" => ReadRechargeTraitFact(json),
                "StunBreak" => ReadStunBreakTraitFact(json),
                "Time" => ReadTimeTraitFact(json),
                "Unblockable" => ReadUnblockableTraitFact(json),
                // BUG: Life Force Cost trait facts don't have a 'type'
                _ when json.TryGetProperty("percent", out _) => ReadLifeForceCostTraitFact(json),
                _ => throw new JsonException($"Could not find a type derived from '{typeName}' for value '{discriminator}'")
            };
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;

        private LifeForceCostTraitFact ReadLifeForceCostTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int percent = default;
            bool percentSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(LifeForceCostTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("percent"))
                {
                    percentSeen = true;
                    percent = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!percentSeen)
            {
                throw new JsonException($"Missing required property 'percent' for object of type '{typeName}'.");
            }

            return new LifeForceCostTraitFact
            {
                Text = text,
                Icon = icon,
                Percent = percent,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private UnblockableTraitFact ReadUnblockableTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            bool value = default;
            bool valueSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(UnblockableTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetBoolean();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            return new UnblockableTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private TimeTraitFact ReadTimeTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            TimeSpan duration = default;
            bool durationSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(TimeTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("duration"))
                {
                    durationSeen = true;
                    duration = TimeSpan.FromSeconds(member.Value.GetInt32());
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!durationSeen)
            {
                throw new JsonException($"Missing required property 'duration' for object of type '{typeName}'.");
            }

            return new TimeTraitFact
            {
                Text = text,
                Icon = icon,
                Duration = duration,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private StunBreakTraitFact ReadStunBreakTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            bool value = default;
            bool valueSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(StunBreakTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetBoolean();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            return new StunBreakTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private RangeTraitFact ReadRangeTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int value = default;
            bool valueSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(RangeTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            return new RangeTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private RadiusTraitFact ReadRadiusTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int distance = default;
            bool distanceSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(RadiusTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("distance"))
                {
                    distanceSeen = true;
                    distance = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!distanceSeen)
            {
                throw new JsonException($"Missing required property 'distance' for object of type '{typeName}'.");
            }

            return new RadiusTraitFact
            {
                Text = text,
                Icon = icon,
                Distance = distance,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private BuffPrefix ReadBuffPrefix(in JsonElement json)
        {
            string text = "";
            bool textSeen = default;

            var icon = "";
            bool iconSeen = default;

            string? status = default;

            string? description = default;

            var typeName = nameof(BuffPrefix);
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("text"))
                {
                    textSeen = true;
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("status"))
                {
                    status = member.Value.GetString();
                }
                else if (member.NameEquals("description"))
                {
                    description = member.Value.GetString();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!textSeen)
            {
                throw new JsonException($"Missing required property 'text' for object of type '{typeName}'.");
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            return new BuffPrefix { Text = text, Icon = icon, Status = status, Description = description };
        }

        private PrefixedBuffTraitFact ReadPrefixedBuffTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            TimeSpan? duration = default;

            string status = "";

            string? description = default;

            int? applyCount = default;

            BuffPrefix prefix = new BuffPrefix();
            bool prefixSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(PrefixedBuffTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("duration"))
                {
                    duration = TimeSpan.FromSeconds(member.Value.GetInt32());
                }
                else if (member.NameEquals("status"))
                {
                    status = member.Value.GetString();
                }
                else if (member.NameEquals("description"))
                {
                    description = member.Value.GetString();
                }
                else if (member.NameEquals("apply_count"))
                {
                    applyCount = member.Value.GetInt32();
                }
                else if (member.NameEquals("prefix"))
                {
                    prefixSeen = true;
                    prefix = ReadBuffPrefix(member.Value);
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!prefixSeen)
            {
                throw new JsonException($"Missing required property 'prefix' for object of type '{typeName}'.");
            }

            return new PrefixedBuffTraitFact
            {
                Text = text,
                Icon = icon,
                Duration = duration,
                Status = status,
                Description = description,
                ApplyCount = applyCount,
                Prefix = prefix,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private PercentTraitFact ReadPercentTraitFact(in JsonElement json)
        {
            string? text = default;

            string icon = "";
            bool iconSeen = default;

            double percent = default;
            bool percentSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(PercentTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("percent"))
                {
                    percentSeen = true;
                    percent = member.Value.GetDouble();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!percentSeen)
            {
                throw new JsonException($"Missing required property 'percent' for object of type '{typeName}'.");
            }

            return new PercentTraitFact
            {
                Text = text,
                Icon = icon,
                Percent = percent,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private NumberTraitFact ReadNumberTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int value = default;
            bool valueSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(NumberTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            return new NumberTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private NoDataTraitFact ReadNoDataTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(NoDataTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            return new NoDataTraitFact { Text = text, Icon = icon, RequiresTrait = requiresTrait, Overrides = overrides };
        }

        private DistanceTraitFact ReadDistanceTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int distance = default;
            bool distanceSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(DistanceTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("distance"))
                {
                    distanceSeen = true;
                    distance = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!distanceSeen)
            {
                throw new JsonException($"Missing required property 'distance' for object of type '{typeName}'.");
            }

            return new DistanceTraitFact
            {
                Text = text,
                Icon = icon,
                Distance = distance,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private DamageTraitFact ReadDamageTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int hitCount = default;
            bool hitCountSeen = default;

            double damageMultiplier = default;
            bool damageMultiplierSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(DamageTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("hit_count"))
                {
                    hitCountSeen = true;
                    hitCount = member.Value.GetInt32();
                }
                else if (member.NameEquals("dmg_multiplier"))
                {
                    damageMultiplierSeen = true;
                    damageMultiplier = member.Value.GetDouble();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!hitCountSeen)
            {
                throw new JsonException($"Missing required property 'hit_count' for object of type '{typeName}'.");
            }

            if (!damageMultiplierSeen)
            {
                throw new JsonException($"Missing required property 'dmg_multiplier' for object of type '{typeName}'.");
            }

            return new DamageTraitFact
            {
                Text = text,
                Icon = icon,
                HitCount = hitCount,
                DamageMultiplier = damageMultiplier,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private ComboFinisherTraitFact ReadComboFinisherTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int percent = default;
            bool percentSeen = default;

            ComboFinisherName finisherType = default;
            bool finisherTypeSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(ComboFinisherTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("percent"))
                {
                    percentSeen = true;
                    percent = member.Value.GetInt32();
                }
                else if (member.NameEquals("finisher_type"))
                {
                    finisherTypeSeen = true;
                    finisherType = Enum.Parse<ComboFinisherName>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!percentSeen)
            {
                throw new JsonException($"Missing required property 'percent' for object of type '{typeName}'.");
            }

            if (!finisherTypeSeen)
            {
                throw new JsonException($"Missing required property 'finisher_type' for object of type '{typeName}'.");
            }

            return new ComboFinisherTraitFact
            {
                Text = text,
                Icon = icon,
                Percent = percent,
                FinisherName = finisherType,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private ComboFieldTraitFact ReadComboFieldTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            ComboFieldName fieldType = default;
            bool fieldTypeSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(ComboFieldTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("field_type"))
                {
                    fieldTypeSeen = true;
                    fieldType = Enum.Parse<ComboFieldName>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!fieldTypeSeen)
            {
                throw new JsonException($"Missing required property 'field_type' for object of type '{typeName}'.");
            }

            return new ComboFieldTraitFact
            {
                Text = text,
                Icon = icon,
                Field = fieldType,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private BuffConversionTraitFact ReadBuffConversionTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int percent = default;
            bool percentSeen = default;

            TraitTarget source = default;
            bool sourceSeen = default;

            TraitTarget target = default;
            bool targetSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(BuffConversionTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("percent"))
                {
                    percentSeen = true;
                    percent = member.Value.GetInt32();
                }
                else if (member.NameEquals("source"))
                {
                    sourceSeen = true;
                    source = Enum.Parse<TraitTarget>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("target"))
                {
                    targetSeen = true;
                    target = Enum.Parse<TraitTarget>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!percentSeen)
            {
                throw new JsonException($"Missing required property 'percent' for object of type '{typeName}'.");
            }

            if (!sourceSeen)
            {
                throw new JsonException($"Missing required property 'source' for object of type '{typeName}'.");
            }

            if (!targetSeen)
            {
                throw new JsonException($"Missing required property 'target' for object of type '{typeName}'.");
            }

            return new BuffConversionTraitFact
            {
                Text = text,
                Icon = icon,
                Percent = percent,
                Source = source,
                Target = target,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private BuffTraitFact ReadBuffTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            TimeSpan? duration = default;

            string status = "";
            bool statusSeen = default;

            string? description = default;

            int? applyCount = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(BuffTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("duration"))
                {
                    duration = TimeSpan.FromSeconds(member.Value.GetInt32());
                }
                else if (member.NameEquals("status"))
                {
                    statusSeen = true;
                    status = member.Value.GetString();
                }
                else if (member.NameEquals("description"))
                {
                    description = member.Value.GetString();
                }
                else if (member.NameEquals("apply_count"))
                {
                    applyCount = member.Value.GetInt32();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!statusSeen)
            {
                throw new JsonException($"Missing required property 'status' for object of type '{typeName}'.");
            }

            return new BuffTraitFact
            {
                Text = text,
                Icon = icon,
                Duration = duration,
                Status = status,
                Description = description,
                ApplyCount = applyCount,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private RechargeTraitFact ReadRechargeTraitFact(in JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            double value = default;
            bool valueSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(RechargeTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetDouble();
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            return new RechargeTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }

        private AttributeAdjustTraitFact ReadAttributeAdjustTraitFact(JsonElement json)
        {
            string? text = default;

            var icon = "";
            bool iconSeen = default;

            int value = default;
            bool valueSeen = default;

            TraitTarget target = default;
            bool targetSeen = default;

            int? requiresTrait = default;
            int? overrides = default;

            var typeName = nameof(AttributeAdjustTraitFact);
            foreach (var member in json.EnumerateObject())
            {
                // Ignore discriminator field
                if (member.NameEquals("type"))
                {
                    continue;
                }

                if (member.NameEquals("text"))
                {
                    text = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("value"))
                {
                    valueSeen = true;
                    value = member.Value.GetInt32();
                }
                else if (member.NameEquals("target"))
                {
                    targetSeen = true;
                    target = Enum.Parse<TraitTarget>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("requires_trait"))
                {
                    requiresTrait = member.Value.GetInt32();
                }
                else if (member.NameEquals("overrides"))
                {
                    overrides = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!valueSeen)
            {
                throw new JsonException($"Missing required property 'value' for object of type '{typeName}'.");
            }

            if (!targetSeen)
            {
                throw new JsonException($"Missing required property 'target' for object of type '{typeName}'.");
            }

            return new AttributeAdjustTraitFact
            {
                Text = text,
                Icon = icon,
                Value = value,
                Target = target,
                RequiresTrait = requiresTrait,
                Overrides = overrides
            };
        }
    }
}
