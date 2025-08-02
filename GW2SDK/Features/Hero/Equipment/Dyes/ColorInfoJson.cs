using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class ColorInfoJson
{
    public static ColorInfo GetColorInfo(this in JsonElement json)
    {
        RequiredMember brightness = "brightness";
        RequiredMember contrast = "contrast";
        RequiredMember hue = "hue";
        RequiredMember saturation = "saturation";
        RequiredMember lightness = "lightness";
        RequiredMember rgb = "rgb";

        foreach (var member in json.EnumerateObject())
        {
            if (brightness.Match(member))
            {
                brightness = member;
            }
            else if (contrast.Match(member))
            {
                contrast = member;
            }
            else if (hue.Match(member))
            {
                hue = member;
            }
            else if (saturation.Match(member))
            {
                saturation = member;
            }
            else if (lightness.Match(member))
            {
                lightness = member;
            }
            else if (rgb.Match(member))
            {
                rgb = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ColorInfo
        {
            Brightness = brightness.Map(static (in JsonElement value) => value.GetInt32()),
            Contrast = contrast.Map(static (in JsonElement value) => value.GetDouble()),
            Hue = hue.Map(static (in JsonElement value) => value.GetInt32()),
            Saturation = saturation.Map(static (in JsonElement value) => value.GetDouble()),
            Lightness = lightness.Map(static (in JsonElement value) => value.GetDouble()),
            Rgb = rgb.Map(static (in JsonElement value) => value.GetColor())
        };
    }
}
