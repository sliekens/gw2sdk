using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record TrainingObjective
    {
        public int Id { get; init; }

        public int Spent { get; init; }

        public bool Done { get; init; }
    }
}