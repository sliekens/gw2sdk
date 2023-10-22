using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

internal static class SkillFactJson
{
    public static SkillFact GetSkillFact(
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
            return json.GetPercentSkillFact(
                missingMemberBehavior,
                out requiresTrait,
                out overrides
            );
        }

        switch (type.GetString())
        {
            case "AttributeAdjust":
                return json.GetAttributeAdjustSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboField":
                return json.GetComboFieldSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboFinisher":
                return json.GetComboFinisherSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Buff":
                return json.GetBuffSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Damage":
                return json.GetDamageSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Distance":
                return json.GetDistanceSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Duration":
                return json.GetDurationSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "HealingAdjust":
                return json.GetHealingAdjustSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "NoData":
                return json.GetNoDataSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Number":
                return json.GetNumberSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Percent":
                return json.GetPercentSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "PrefixedBuff":
                return json.GetPrefixedBuffSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Radius":
                return json.GetRadiusSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Range":
                return json.GetRangeSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Recharge":
                return json.GetRechargeSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "StunBreak":
                return json.GetStunBreakSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Time":
                return json.GetTimeSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Unblockable":
                return json.GetUnblockableSkillFact(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
        }

        RequiredMember text = "text";
        RequiredMember icon = "icon";

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

        return new SkillFact
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired())
        };
    }
}
