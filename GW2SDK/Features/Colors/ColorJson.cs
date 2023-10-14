using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class ColorJson
{
    public static Color GetColor(this JsonElement json)
    {
        if (json.GetArrayLength() < 3)
        {
            throw new InvalidOperationException($"Missing RGB value.");
        }
        else if (json.GetArrayLength() > 3)
        {
            throw new InvalidOperationException($"Unexpected RGB value.");
        }

        var red = json[0].GetInt32();
        var green = json[1].GetInt32();
        var blue = json[2].GetInt32();
        return Color.FromArgb(red, green, blue);
    }
}
