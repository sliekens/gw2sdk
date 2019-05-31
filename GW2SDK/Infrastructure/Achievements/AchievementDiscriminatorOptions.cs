using System;
using System.Collections.Generic;
using GW2SDK.Features.Achievements;

namespace GW2SDK.Infrastructure.Achievements
{
    public sealed class AchievementDiscriminatorOptions : DiscriminatorOptions
    {
        public override Type BaseType => typeof(Achievement);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(Achievement));
            yield return ("ItemSet", typeof(ItemSetAchievement));
        }

        public override object Create(Type objectType)
        {
            if (objectType == typeof(Achievement))
            {
                return new Achievement();
            }

            if (objectType == typeof(ItemSetAchievement))
            {
                return new ItemSetAchievement();
            }

            return new Achievement();
        }
    }
}
