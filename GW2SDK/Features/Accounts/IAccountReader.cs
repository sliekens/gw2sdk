using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public interface IAccountReader : IJsonReader<Account>
    {
    }
}
