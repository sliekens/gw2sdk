using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class ColorJson
{
    public static Color GetColor(this JsonElement value)
    {
        if (value.GetArrayLength() != 3)
        {
            throw new InvalidOperationException($"Missing RGB value.");
        }

        var red = value[0].GetInt32();
        var green = value[1].GetInt32();
        var blue = value[2].GetInt32();
        return Color.FromArgb(red, green, blue);
    }
}
