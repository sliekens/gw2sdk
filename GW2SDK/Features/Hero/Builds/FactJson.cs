using System.Text.Json;
using GuildWars2.Hero.Builds.Facts;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class FactJson
{
    public static Fact GetFact(this in JsonElement json, out int? requiresTrait, out int? overrides)
    {
        requiresTrait = null;
        overrides = null;

        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "AttributeAdjust":
                    return json.GetAttributeAdjustment(out requiresTrait, out overrides);
                case "Buff":
                    return json.GetBuff(out requiresTrait, out overrides);
                case "BuffConversion": // Traits only it seems
                    return json.GetAttributeConversion(out requiresTrait, out overrides);
                case "ComboField":
                    return json.GetComboField(out requiresTrait, out overrides);
                case "ComboFinisher":
                    return json.GetComboFinisher(out requiresTrait, out overrides);
                case "Damage":
                    return json.GetDamage(out requiresTrait, out overrides);
                case "Distance":
                    return json.GetDistance(out requiresTrait, out overrides);
                case "Duration":
                    return json.GetDuration(out requiresTrait, out overrides);
                case "HealingAdjust":
                    return json.GetHealingAdjust(out requiresTrait, out overrides);
                case "NoData":
                    return json.GetNoData(out requiresTrait, out overrides);
                case "Number":
                    return json.GetNumber(out requiresTrait, out overrides);
                case "Percent":
                    return json.GetPercentage(out requiresTrait, out overrides);
                case "PrefixedBuff":
                    return json.GetPrefixedBuff(out requiresTrait, out overrides);
                case "Radius":
                    return json.GetRadius(out requiresTrait, out overrides);
                case "Range":
                    return json.GetRange(out requiresTrait, out overrides);
                case "Recharge":
                    return json.GetRecharge(out requiresTrait, out overrides);
                case "StunBreak":
                    return json.GetStunBreak(out requiresTrait, out overrides);
                case "Time":
                    return json.GetTime(out requiresTrait, out overrides);
                case "Unblockable":
                    return json.GetUnblockable(out requiresTrait, out overrides);
            }
        }

        // BUG: Life Force Cost is missing a type property, but we can treat it as Percent
        if (discriminator.ValueKind == JsonValueKind.Undefined
            && json.TryGetProperty("percent", out _))
        {
            return json.GetPercentage(out requiresTrait, out overrides);
        }

        OptionalMember text = "text";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (text.Match(member))
            {
                text = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        return new Fact
        {
            Text = text.Map(static (in JsonElement value) => value.GetString()) ?? "",
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute)
        };
    }
}
