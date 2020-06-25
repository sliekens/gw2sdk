using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Achievements.Impl
{
    internal sealed class AchievementRewardDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(AchievementReward);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Title", typeof(TitleReward));
            yield return ("Mastery", typeof(MasteryReward));
            yield return ("Item", typeof(ItemReward));
            yield return ("Coins", typeof(CoinsReward));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(TitleReward)) return new TitleReward();
            if (discriminatedType == typeof(MasteryReward)) return new MasteryReward();
            if (discriminatedType == typeof(ItemReward)) return new ItemReward();
            if (discriminatedType == typeof(CoinsReward)) return new CoinsReward();
            return new AchievementReward();
        }
    }
}
