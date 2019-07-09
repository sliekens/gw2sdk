﻿using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
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
