using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Color
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public int[] BaseRgb { get; init; } = new int[0];

        public ColorInfo Cloth { get; init; } = new();

        public ColorInfo Leather { get; init; } = new();

        public ColorInfo Metal { get; init; } = new();

        public ColorInfo? Fur { get; init; }

        public int? Item { get; init; }

        public ColorCategoryName[] Categories { get; init; } = new ColorCategoryName[0];
    }
}
