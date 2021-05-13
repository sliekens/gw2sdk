using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public interface ISubtokenReader : IJsonReader<CreatedSubtoken>
    {
    }
}
