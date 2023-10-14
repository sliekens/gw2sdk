using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class ColorJson
{
    public static Color GetColor(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> red = new("red");
        RequiredMember<int> green = new("green");
        RequiredMember<int> blue = new("blue");

        foreach (var entry in json.EnumerateArray())
        {
            if (red.IsMissing)
            {
                red.Value = entry;
            }
            else if (green.IsMissing)
            {
                green.Value = entry;
            }
            else if (blue.IsMissing)
            {
                blue.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return Color.FromArgb(
            red.GetValue(),
            green.GetValue(),
            blue.GetValue()
        );
    }
}
