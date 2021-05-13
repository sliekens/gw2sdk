using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public interface ICurrencyReader : IJsonReader<Currency>
    {
        IJsonReader<int> Id { get; }
    }
}
