using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Skins;
using Newtonsoft.Json;

namespace GW2SDK.Features.Skins
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(GatheringToolSkinDiscriminatorOptions))]
    public class GatheringToolSkin : Skin
    {
    }
}