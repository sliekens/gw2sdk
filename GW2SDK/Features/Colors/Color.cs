using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Colors
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Color
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int[] BaseRgb { get; set; } = new int[0];

        public ColorInfo Cloth { get; set; } = new ColorInfo();

        public ColorInfo Leather { get; set; } = new ColorInfo();

        public ColorInfo Metal { get; set; } = new ColorInfo();

        public ColorInfo? Fur { get; set; }

        public int? Item { get; set; }

        public ColorCategoryName[] Categories { get; set; } = new ColorCategoryName[0];
    }
}
