using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Training
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public TrainingCategory Category { get; init; }

        public TrainingObjective[] Track { get; set; } = Array.Empty<TrainingObjective>();
    }
}