using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Achievements;
using Newtonsoft.Json;

namespace GW2SDK.Features.Achievements
{
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(AchievementRewardDiscriminatorOptions))]
    public class AchievementReward
    {
    }
}
