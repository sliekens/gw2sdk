using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Specializations
{
    [PublicAPI]
    public interface ISpecializationReader : IJsonReader<Specialization>
    {
        IJsonReader<int> Id { get; }
    }
}
