using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Colors
{
    [DataTransferObject]
    public sealed class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int[] BaseRgb { get; set; }

        public ColorInfo Cloth { get; set; }

        public ColorInfo Leather { get; set; }

        public ColorInfo Metal { get; set; }

        public ColorInfo Fur { get; set; }

        public int? Item { get; set; }

        public ColorCategoryName[] Categories { get; set; }
    }
}
