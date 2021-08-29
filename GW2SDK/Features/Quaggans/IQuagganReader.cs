using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public interface IQuagganReader
    {
        IJsonReader<string> Id { get; }

        IJsonReader<QuagganRef> Quaggan { get; }
    }
}
