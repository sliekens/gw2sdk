using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class ColorInfoJson
{
    public static ColorInfo GetColorInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember brightness = "brightness";
        RequiredMember contrast = "contrast";
        RequiredMember hue = "hue";
        RequiredMember saturation = "saturation";
        RequiredMember lightness = "lightness";
        RequiredMember rgb = "rgb";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(brightness.Name))
            {
                brightness = member;
            }
            else if (member.NameEquals(contrast.Name))
            {
                contrast = member;
            }
            else if (member.NameEquals(hue.Name))
            {
                hue = member;
            }
            else if (member.NameEquals(saturation.Name))
            {
                saturation = member;
            }
            else if (member.NameEquals(lightness.Name))
            {
                lightness = member;
            }
            else if (member.NameEquals(rgb.Name))
            {
                rgb = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ColorInfo
        {
            Brightness = brightness.Select(value => value.GetInt32()),
            Contrast = contrast.Select(value => value.GetDouble()),
            Hue = hue.Select(value => value.GetInt32()),
            Saturation = saturation.Select(value => value.GetDouble()),
            Lightness = lightness.Select(value => value.GetDouble()),
            Rgb = rgb.Select(value => value.GetColor(missingMemberBehavior))
        };
    }
}
