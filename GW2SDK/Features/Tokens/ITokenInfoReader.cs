using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public interface ITokenInfoReader : IJsonReader<TokenInfo>
    {
    }
}
