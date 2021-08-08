using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mumble
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Identity
    {
        public string Name { get; init; } = "";

        public ProfessionName Profession { get; init; }

        public int SpecializationId { get; init; }

        public Race Race { get; init; }

        public int MapId { get; init; }

        public int WorldId { get; init; }

        public int TeamColorId { get; init; }

        public bool Commander { get; init; }

        /// <summary>The vertical field of view.</summary>
        public double FieldOfView { get; init; }

        public UiSize UiSize { get; init; }
    }
}
