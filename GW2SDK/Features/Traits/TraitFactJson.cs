using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TraitFactJson
{
    public static TraitFact GetTraitFact(
        this JsonElement json,
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
            return json.GetPercentTraitFact(
                missingMemberBehavior,
                out requiresTrait,
                out overrides
            );
        }

        switch (type.GetString())
        {
            case "AttributeAdjust":
                return json.GetAttributeAdjustTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Buff":
                return json.GetBuffTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "BuffConversion":
                return json.GetBuffConversionTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboField":
                return json.GetComboFieldTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboFinisher":
                return json.GetComboFinisherTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Damage":
                return json.GetDamageTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Distance":
                return json.GetDistanceTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "NoData":
                return json.GetNoDataTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Number":
                return json.GetNumberTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Percent":
                return json.GetPercentTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "PrefixedBuff":
                return json.GetPrefixedBuffTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Radius":
                return json.GetRadiusTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Range":
                return json.GetRangeTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Recharge":
                return json.GetRechargeTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "StunBreak":
                return json.GetStunBreakTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Time":
                return json.GetTimeTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Unblockable":
                return json.GetUnblockableTraitFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
        }

        OptionalMember text = "text";
        OptionalMember icon = "icon";
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
                text = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitFact
        {
            Text = text.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetString()) ?? ""
        };
    }
}
