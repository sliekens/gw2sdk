using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class SizeJson
{
    public static Size GetDimensions(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember width = new("[0]");
        RequiredMember height = new("[1]");

        foreach (var entry in json.EnumerateArray())
        {
            if (width.IsUndefined)
            {
                width.Value = entry;
            }
            else if (height.IsUndefined)
            {
                height.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return new Size(
            width.Select(value => value.GetInt32()),
            height.Select(value => value.GetInt32())
        );
    }
}
