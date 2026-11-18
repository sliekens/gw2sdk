using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal sealed class FactJsonConverter : JsonConverter<Fact>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "fact";

    public override Fact Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Fact value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Fact Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case Facts.PrefixedBuffJsonConverter.DiscriminatorValue:
                    return Facts.PrefixedBuffJsonConverter.Read(json);
                case Facts.AttributeAdjustmentJsonConverter.DiscriminatorValue:
                    return Facts.AttributeAdjustmentJsonConverter.Read(json);
                case Facts.AttributeConversionJsonConverter.DiscriminatorValue:
                    return Facts.AttributeConversionJsonConverter.Read(json);
                case Facts.BuffJsonConverter.DiscriminatorValue:
                    return Facts.BuffJsonConverter.Read(json);
                case Facts.BuffArrayJsonConverter.DiscriminatorValue:
                    return Facts.BuffArrayJsonConverter.Read(json);
                case Facts.ComboFieldJsonConverter.DiscriminatorValue:
                    return Facts.ComboFieldJsonConverter.Read(json);
                case Facts.ComboFinisherJsonConverter.DiscriminatorValue:
                    return Facts.ComboFinisherJsonConverter.Read(json);
                case Facts.DamageJsonConverter.DiscriminatorValue:
                    return Facts.DamageJsonConverter.Read(json);
                case Facts.DistanceJsonConverter.DiscriminatorValue:
                    return Facts.DistanceJsonConverter.Read(json);
                case Facts.DurationJsonConverter.DiscriminatorValue:
                    return Facts.DurationJsonConverter.Read(json);
                case Facts.HealingAdjustJsonConverter.DiscriminatorValue:
                    return Facts.HealingAdjustJsonConverter.Read(json);
                case Facts.NoDataJsonConverter.DiscriminatorValue:
                    return Facts.NoDataJsonConverter.Read(json);
                case Facts.NumberJsonConverter.DiscriminatorValue:
                    return Facts.NumberJsonConverter.Read(json);
                case Facts.PercentageJsonConverter.DiscriminatorValue:
                    return Facts.PercentageJsonConverter.Read(json);
                case Facts.RadiusJsonConverter.DiscriminatorValue:
                    return Facts.RadiusJsonConverter.Read(json);
                case Facts.RangeJsonConverter.DiscriminatorValue:
                    return Facts.RangeJsonConverter.Read(json);
                case Facts.RechargeJsonConverter.DiscriminatorValue:
                    return Facts.RechargeJsonConverter.Read(json);
                case Facts.StunBreakJsonConverter.DiscriminatorValue:
                    return Facts.StunBreakJsonConverter.Read(json);
                case Facts.TimeJsonConverter.DiscriminatorValue:
                    return Facts.TimeJsonConverter.Read(json);
                case Facts.UnblockableJsonConverter.DiscriminatorValue:
                    return Facts.UnblockableJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        return new Fact
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute)
        };
    }

    public static void Write(Utf8JsonWriter writer, Fact value)
    {
        if (value is Facts.PrefixedBuff prefixedBuff)
        {
            Facts.PrefixedBuffJsonConverter.Write(writer, prefixedBuff);
        }
        else if (value is Facts.AttributeAdjustment attributeAdjustment)
        {
            Facts.AttributeAdjustmentJsonConverter.Write(writer, attributeAdjustment);
        }
        else if (value is Facts.AttributeConversion attributeConversion)
        {
            Facts.AttributeConversionJsonConverter.Write(writer, attributeConversion);
        }
        else if (value is Facts.Buff buff)
        {
            Facts.BuffJsonConverter.Write(writer, buff);
        }
        else if (value is Facts.BuffArray buffArray)
        {
            Facts.BuffArrayJsonConverter.Write(writer, buffArray);
        }
        else if (value is Facts.ComboField comboField)
        {
            Facts.ComboFieldJsonConverter.Write(writer, comboField);
        }
        else if (value is Facts.ComboFinisher comboFinisher)
        {
            Facts.ComboFinisherJsonConverter.Write(writer, comboFinisher);
        }
        else if (value is Facts.Damage damage)
        {
            Facts.DamageJsonConverter.Write(writer, damage);
        }
        else if (value is Facts.Distance distance)
        {
            Facts.DistanceJsonConverter.Write(writer, distance);
        }
        else if (value is Facts.Duration duration)
        {
            Facts.DurationJsonConverter.Write(writer, duration);
        }
        else if (value is Facts.HealingAdjust healingAdjust)
        {
            Facts.HealingAdjustJsonConverter.Write(writer, healingAdjust);
        }
        else if (value is Facts.NoData noData)
        {
            Facts.NoDataJsonConverter.Write(writer, noData);
        }
        else if (value is Facts.Number number)
        {
            Facts.NumberJsonConverter.Write(writer, number);
        }
        else if (value is Facts.Percentage percentage)
        {
            Facts.PercentageJsonConverter.Write(writer, percentage);
        }
        else if (value is Facts.Radius radius)
        {
            Facts.RadiusJsonConverter.Write(writer, radius);
        }
        else if (value is Facts.Range range)
        {
            Facts.RangeJsonConverter.Write(writer, range);
        }
        else if (value is Facts.Recharge recharge)
        {
            Facts.RechargeJsonConverter.Write(writer, recharge);
        }
        else if (value is Facts.StunBreak stunBreak)
        {
            Facts.StunBreakJsonConverter.Write(writer, stunBreak);
        }
        else if (value is Facts.Time time)
        {
            Facts.TimeJsonConverter.Write(writer, time);
        }
        else if (value is Facts.Unblockable unblockable)
        {
            Facts.UnblockableJsonConverter.Write(writer, unblockable);
        }
        else if (value.GetType() == typeof(Fact))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteString("text", value.Text);
            if (value.IconUrl is { } presentIconUrl)
            {
                writer.WriteString("icon_url", presentIconUrl.OriginalString);
            }
            else
            {
                writer.WriteNull("icon_url");
            }
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
