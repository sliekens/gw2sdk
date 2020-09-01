using System.Text.Json;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class TraitFactDiscriminatingJsonReader : IJsonReader<TraitFact>
    {
        private readonly UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        public TraitFactDiscriminatingJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            _unexpectedPropertyBehavior = unexpectedPropertyBehavior;
        }

        public TraitFact Read(in JsonElement element, in JsonPath path)
        {
            if (element.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException($"Value is not an object.", path.ToString(), null, null);
            }

            var discriminator = element.TryGetProperty("type", out var type) ? type.GetString() : "";
            return discriminator switch
            {
                "AttributeAdjust" => AttributeAdjustTraitFactJsonReader.Instance.Read(element, path),
                "Buff" => BuffTraitFactJsonReader.Instance.Read(element, path),
                "BuffConversion" => BuffConversionTraitFactJsonReader.Instance.Read(element, path),
                "ComboField" => ComboFieldTraitFactJsonReader.Instance.Read(element, path),
                "ComboFinisher" => ComboFinisherTraitFactJsonReader.Instance.Read(element, path),
                "Damage" => DamageTraitFactJsonReader.Instance.Read(element, path),
                "Distance" => DistanceTraitFactJsonReader.Instance.Read(element, path),
                "NoData" => NoDataTraitFactJsonReader.Instance.Read(element, path),
                "Number" => NumberTraitFactJsonReader.Instance.Read(element, path),
                "Percent" => PercentTraitFactJsonReader.Instance.Read(element, path),
                "PrefixedBuff" => PrefixedBuffTraitFactJsonReader.Instance.Read(element, path),
                "Radius" => RadiusTraitFactJsonReader.Instance.Read(element, path),
                "Range" => RangeTraitFactJsonReader.Instance.Read(element, path),
                "Recharge" => RechargeTraitFactJsonReader.Instance.Read(element, path),
                "StunBreak" => StunBreakTraitFactJsonReader.Instance.Read(element, path),
                "Time" => TimeTraitFactJsonReader.Instance.Read(element, path),
                "Unblockable" => UnblockableTraitFactJsonReader.Instance.Read(element, path),
                // BUG: Life Force Cost trait facts don't have a 'type'
                _ when element.TryGetProperty("percent", out _) => LifeForceCostTraitFactJsonReader.Instance.Read(element, path),
                _ when _unexpectedPropertyBehavior == UnexpectedPropertyBehavior.Ignore => TraitFactJsonReader<TraitFact>.Instance.Read(element, path),
                _ => throw new JsonException($"Could not find a type derived from 'TraitFact' for value '{discriminator}'.", path.ToString(), null, null)
            };

        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}
