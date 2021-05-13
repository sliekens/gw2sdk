using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Builds
{
    [PublicAPI]
    public interface IBuildReader : IJsonReader<Build>
    {
    }
}
