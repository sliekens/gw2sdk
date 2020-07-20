using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorInfoJsonReader : JsonObjectReader<ColorInfo>
    {
        public static JsonObjectReader<ColorInfo> Instance { get; } = new ColorInfoJsonReader();

        private ColorInfoJsonReader()
        {
            Map("brightness", to => to.Brightness);
            Map("contrast",   to => to.Contrast);
            Map("hue",        to => to.Hue);
            Map("saturation", to => to.Saturation);
            Map("lightness",  to => to.Lightness);
            Map("rgb",        to => to.Rgb);
        }
    }
}