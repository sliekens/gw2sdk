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
        RequiredMember red = new("[0]");
        RequiredMember green = new("[1]");
        RequiredMember blue = new("[2]");

        foreach (var entry in json.EnumerateArray())
        {
            if (red.IsUndefined)
            {
                red.Value = entry;
            }
            else if (green.IsUndefined)
            {
                green.Value = entry;
            }
            else if (blue.IsUndefined)
            {
                blue.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return Color.FromArgb(
            red.Select(value => value.GetInt32()),
            green.Select(value => value.GetInt32()),
            blue.Select(value => value.GetInt32())
        );
    }
}
