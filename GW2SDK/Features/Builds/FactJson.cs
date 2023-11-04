using System.Text.Json;
using GuildWars2.Builds.Facts;
using GuildWars2.Json;

namespace GuildWars2.Builds;

internal static class FactJson
{
    public static Fact GetFact(
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
            return json.GetPercentage(missingMemberBehavior, out requiresTrait, out overrides);
        }

        switch (type.GetString())
        {
            case "AttributeAdjust":
                return json.GetAttributeAdjustment(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Buff":
                return json.GetBuff(missingMemberBehavior, out requiresTrait, out overrides);
            case "BuffConversion": // Traits only it seems
                return json.GetAttributeConversion(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "ComboField":
                return json.GetComboField(missingMemberBehavior, out requiresTrait, out overrides);
            case "ComboFinisher":
                return json.GetComboFinisher(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Damage":
                return json.GetDamage(missingMemberBehavior, out requiresTrait, out overrides);
            case "Distance":
                return json.GetDistance(missingMemberBehavior, out requiresTrait, out overrides);
            case "Duration":
                return json.GetDuration(missingMemberBehavior, out requiresTrait, out overrides);
            case "HealingAdjust":
                return json.GetHealingAdjust(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "NoData":
                return json.GetNoData(missingMemberBehavior, out requiresTrait, out overrides);
            case "Number":
                return json.GetNumber(missingMemberBehavior, out requiresTrait, out overrides);
            case "Percent":
                return json.GetPercentage(missingMemberBehavior, out requiresTrait, out overrides);
            case "PrefixedBuff":
                return json.GetPrefixedBuff(
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                );
            case "Radius":
                return json.GetRadius(missingMemberBehavior, out requiresTrait, out overrides);
            case "Range":
                return json.GetRange(missingMemberBehavior, out requiresTrait, out overrides);
            case "Recharge":
                return json.GetRecharge(missingMemberBehavior, out requiresTrait, out overrides);
            case "StunBreak":
                return json.GetStunBreak(missingMemberBehavior, out requiresTrait, out overrides);
            case "Time":
                return json.GetTime(missingMemberBehavior, out requiresTrait, out overrides);
            case "Unblockable":
                return json.GetUnblockable(missingMemberBehavior, out requiresTrait, out overrides);
        }

        RequiredMember text = "text";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == "requires_trait")
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.Name == "overrides")
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.Name == text.Name)
            {
                text = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Fact
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired())
        };
    }
}
