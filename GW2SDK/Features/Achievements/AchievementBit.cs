using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Achievements;
using Newtonsoft.Json;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(AchievementBitDiscriminatorOptions))]
    public class AchievementBit
    {
        
    }
}