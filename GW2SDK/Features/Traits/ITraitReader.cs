using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public interface ITraitReader : IJsonReader<Trait>
    {
        IJsonReader<int> Id { get; }
    }
}
