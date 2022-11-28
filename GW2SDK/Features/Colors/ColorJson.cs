using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;

namespace GuildWars2.Colors;

[PublicAPI]
public static class ColorJson
{
    public static Color GetColor(
        this JsonElement value,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // TODO: use MissingMemberBehavior
        var red = value[0].GetInt32();
        var green = value[1].GetInt32();
        var blue = value[2].GetInt32();
        return Color.FromArgb(red, green, blue);
    }
}
