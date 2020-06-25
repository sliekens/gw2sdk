using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Achievements.Impl
{
    internal sealed class AchievementBitDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(AchievementBit);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Text", typeof(AchievementTextBit));
            yield return ("Minipet", typeof(AchievementMinipetBit));
            yield return ("Item", typeof(AchievementItemBit));
            yield return ("Skin", typeof(AchievementSkinBit));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(AchievementTextBit)) return new AchievementTextBit();
            if (discriminatedType == typeof(AchievementMinipetBit)) return new AchievementMinipetBit();
            if (discriminatedType == typeof(AchievementItemBit)) return new AchievementItemBit();
            if (discriminatedType == typeof(AchievementSkinBit)) return new AchievementSkinBit();
            return new AchievementBit();
        }
    }
}
