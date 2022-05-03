using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public static class TraitReader
{
    public static Trait GetTrait(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> tier = new("tier");
        RequiredMember<int> order = new("order");
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<TraitSlot> slot = new("slot");
        OptionalMember<TraitFact> facts = new("facts");
        OptionalMember<CompoundTraitFact> traitedFacts = new("traited_facts");
        OptionalMember<TraitSkill> skills = new("skills");
        RequiredMember<int> specialization = new("specialization");
        RequiredMember<string> icon = new("icon");

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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Trait
        {
            Id = id.GetValue(),
            Tier = tier.GetValue(),
            Order = order.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Slot = slot.GetValue(missingMemberBehavior),
            Icon = icon.GetValue(),
            SpezializationId = specialization.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadTraitFact(value, missingMemberBehavior, out _, out _)
                ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadCompoundTraitFact(value, missingMemberBehavior)
                ),
            Skills = skills.SelectMany(value => ReadTraitSkill(value, missingMemberBehavior))
        };
    }

    private static TraitFact ReadTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        // BUG: Life Force Cost is missing a type property but we can treat it as Percent
        if (!json.TryGetProperty("type", out var type) && json.TryGetProperty("percent", out _))
        {
            return ReadPercentTraitFact(
                json,
                missingMemberBehavior,
                out requiresTrait,
                out overrides
            );
        }

        switch (type.GetString())
        {
            case "AttributeAdjust":
                return ReadAttributeAdjustTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Buff":
                return ReadBuffTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "BuffConversion":
                return ReadBuffConversionTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboField":
                return ReadComboFieldTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboFinisher":
                return ReadComboFinisherTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Damage":
                return ReadDamageTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Distance":
                return ReadDistanceTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "NoData":
                return ReadNoDataTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Number":
                return ReadNumberTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Percent":
                return ReadPercentTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "PrefixedBuff":
                return ReadPrefixedBuffTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Radius":
                return ReadRadiusTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Range":
                return ReadRangeTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Recharge":
                return ReadRechargeTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "StunBreak":
                return ReadStunBreakTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Time":
                return ReadTimeTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Unblockable":
                return ReadUnblockableTraitFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
        }

        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty()
        };
    }

    private static AttributeAdjustTraitFact ReadAttributeAdjustTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> value = new("value");
        RequiredMember<AttributeAdjustTarget> target = new("target");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("AttributeAdjust"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeAdjustTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue(),
            Target = target.GetValue(missingMemberBehavior)
        };
    }

    private static BuffTraitFact ReadBuffTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        NullableMember<TimeSpan> duration = new("duration");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        NullableMember<int> applyCount = new("apply_count");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Buff"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
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

    private static BuffConversionTraitFact ReadBuffConversionTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> percent = new("percent");
        RequiredMember<AttributeAdjustTarget> source = new("source");
        RequiredMember<AttributeAdjustTarget> target = new("target");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("BuffConversion"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffConversionTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Percent = percent.GetValue(),
            Source = source.GetValue(missingMemberBehavior),
            Target = target.GetValue(missingMemberBehavior)
        };
    }

    private static ComboFieldTraitFact ReadComboFieldTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<ComboFieldName> fieldType = new("field_type");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboField"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFieldTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Field = fieldType.GetValue(missingMemberBehavior)
        };
    }

    private static ComboFinisherTraitFact ReadComboFinisherTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> percent = new("percent");
        RequiredMember<ComboFinisherName> finisherType = new("finisher_type");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboFinisher"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFinisherTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Percent = percent.GetValue(),
            FinisherName = finisherType.GetValue(missingMemberBehavior)
        };
    }

    private static DamageTraitFact ReadDamageTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> hitCount = new("hit_count");
        RequiredMember<double> damageMultiplier = new("dmg_multiplier");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Damage"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
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

    private static DistanceTraitFact ReadDistanceTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> distance = new("distance");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Distance"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DistanceTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Distance = distance.GetValue()
        };
    }

    private static NoDataTraitFact ReadNoDataTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("NoData"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NoDataTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty()
        };
    }

    private static NumberTraitFact ReadNumberTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> value = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Number"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NumberTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue()
        };
    }

    private static PercentTraitFact ReadPercentTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<double> percent = new("percent");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Percent"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PercentTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Percent = percent.GetValue()
        };
    }

    private static PrefixedBuffTraitFact ReadPrefixedBuffTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        NullableMember<TimeSpan> duration = new("duration");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        NullableMember<int> applyCount = new("apply_count");
        RequiredMember<BuffPrefix> prefix = new("prefix");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("PrefixedBuff"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
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

    private static BuffPrefix ReadBuffPrefix(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
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

    private static RadiusTraitFact ReadRadiusTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> distance = new("distance");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Radius"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RadiusTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Distance = distance.GetValue()
        };
    }

    private static RangeTraitFact ReadRangeTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<int> value = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Range"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RangeTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue()
        };
    }

    private static RechargeTraitFact ReadRechargeTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<double> value = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Recharge"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RechargeTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue()
        };
    }

    private static StunBreakTraitFact ReadStunBreakTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<bool> value = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("StunBreak"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StunBreakTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue()
        };
    }

    private static TimeTraitFact ReadTimeTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<TimeSpan> duration = new("duration");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Time"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TimeTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble()))
        };
    }

    private static UnblockableTraitFact ReadUnblockableTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<bool> value = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Unblockable"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UnblockableTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Value = value.GetValue()
        };
    }

    private static CompoundTraitFact ReadCompoundTraitFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var fact = ReadTraitFact(
            json,
            missingMemberBehavior,
            out var requiresTrait,
            out var overrides
        );
        return new CompoundTraitFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }

    private static TraitSkill ReadTraitSkill(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<TraitFact> facts = new("facts");
        OptionalMember<CompoundTraitFact> traitedFacts = new("traited_facts");
        RequiredMember<string> description = new("description");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");
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
                if (missingMemberBehavior == MissingMemberBehavior.Error
                    && member.Value.GetArrayLength() != 0)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitSkill
        {
            Name = name.GetValue(),
            Facts = facts.SelectMany(
                item => ReadTraitFact(item, missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadCompoundTraitFact(value, missingMemberBehavior)
                ),
            Description = description.GetValue(),
            Icon = icon.GetValue(),
            Id = id.GetValue(),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }
}
