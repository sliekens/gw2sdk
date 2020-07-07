using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitFactDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(TraitFact);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("AttributeAdjust", typeof(AttributeAdjustTraitFact));
            yield return ("Buff", typeof(BuffTraitFact));
            yield return ("BuffConversion", typeof(BuffConversionTraitFact));
            yield return ("ComboField", typeof(ComboFieldTraitFact));
            yield return ("ComboFinisher", typeof(ComboFinisherTraitFact));
            yield return ("Damage", typeof(DamageTraitFact));
            yield return ("Distance", typeof(DistanceTraitFact));
            yield return ("NoData", typeof(NoDataTraitFact));
            yield return ("Number", typeof(NumberTraitFact));
            yield return ("Percent", typeof(PercentTraitFact));
            yield return ("PrefixedBuff", typeof(PrefixedBuffTraitFact));
            yield return ("Radius", typeof(RadiusTraitFact));
            yield return ("Range", typeof(RangeTraitFact));
            yield return ("Recharge", typeof(RechargeTraitFact));
            yield return ("StunBreak", typeof(StunBreakTraitFact));
            yield return ("Time", typeof(TimeTraitFact));
            yield return ("Unblockable", typeof(UnblockableTraitFact));
        }

        internal override object CreateInstance(Type discriminatedType) =>
            discriminatedType.Name switch
            {
                nameof(AttributeAdjustTraitFact) => new AttributeAdjustTraitFact(),
                nameof(BuffTraitFact) => new BuffTraitFact(),
                nameof(BuffConversionTraitFact) => new BuffConversionTraitFact(),
                nameof(ComboFieldTraitFact) => new ComboFieldTraitFact(),
                nameof(ComboFinisherTraitFact) => new ComboFinisherTraitFact(),
                nameof(DamageTraitFact) => new DamageTraitFact(),
                nameof(DistanceTraitFact) => new DistanceTraitFact(),
                nameof(NoDataTraitFact) => new NoDataTraitFact(),
                nameof(NumberTraitFact) => new NumberTraitFact(),
                nameof(PercentTraitFact) => new PercentTraitFact(),
                nameof(PrefixedBuffTraitFact) => new PrefixedBuffTraitFact(),
                nameof(RadiusTraitFact) => new RadiusTraitFact(),
                nameof(RangeTraitFact) => new RangeTraitFact(),
                nameof(RechargeTraitFact) => new RechargeTraitFact(),
                nameof(StunBreakTraitFact) => new StunBreakTraitFact(),
                nameof(TimeTraitFact) => new TimeTraitFact(),
                nameof(UnblockableTraitFact) => new UnblockableTraitFact(),
                _ => new TraitFact()
            };
    }
}
