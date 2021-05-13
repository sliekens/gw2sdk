using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public interface IAccountReader : IJsonReader<Account>
    {
    }
}
