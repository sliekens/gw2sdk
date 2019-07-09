using GW2SDK.Achievements.Impl;
using GW2SDK.Annotations;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(AchievementBitDiscriminatorOptions))]
    public class AchievementBit
    {
        
    }
}