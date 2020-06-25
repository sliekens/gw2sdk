using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Achievements.Impl
{
    internal sealed class AchievementDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Achievement);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(Achievement));
            yield return ("ItemSet", typeof(ItemSetAchievement));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Achievement)) return new Achievement();
            if (discriminatedType == typeof(ItemSetAchievement)) return new ItemSetAchievement();
            return new Achievement();
        }
    }
}
