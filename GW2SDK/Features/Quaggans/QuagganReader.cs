using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed class QuagganReader : IQuagganReader
    {
        public IJsonReader<string> Id { get; } = new StringJsonReader();

        public IJsonReader<QuagganRef> Quaggan { get; } = new QuagganRefReader();
    }
}
