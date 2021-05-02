using GW2SDK.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record World
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public WorldPopulation Population { get; init; }
    }
}
