using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorInfoJsonReader : JsonObjectReader<ColorInfo>
    {
        private ColorInfoJsonReader()
        {
            Configure(
                colorInfo =>
                {
                    colorInfo.Map("brightness", to => to.Brightness);
                    colorInfo.Map("contrast",   to => to.Contrast);
                    colorInfo.Map("hue",        to => to.Hue);
                    colorInfo.Map("saturation", to => to.Saturation);
                    colorInfo.Map("lightness",  to => to.Lightness);
                    colorInfo.Map("rgb",        to => to.Rgb);
                }
            );
        }

        public static IJsonReader<ColorInfo> Instance { get; } = new ColorInfoJsonReader();
    }
}
