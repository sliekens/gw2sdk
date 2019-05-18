using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Colors
{
    [DataTransferObject(RootObject = false)]
    public sealed class ColorInfo
    {
        public int Brightness { get; set; }

        public double Contrast { get; set; }

        public int Hue { get; set; }

        public double Saturation { get; set; }

        public double Lightness { get; set; }

        public int[] Rgb { get; set; }
    }
}
