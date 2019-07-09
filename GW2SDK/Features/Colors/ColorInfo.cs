using GW2SDK.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
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
