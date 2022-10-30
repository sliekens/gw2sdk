using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public static class DimensionsReader
{
    public static Size GetDimensions(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // TODO: use MissingMemberBehavior
        var width = json[0].GetInt32();
        var height = json[1].GetInt32();
        return new Size(width, height);
    }
}
