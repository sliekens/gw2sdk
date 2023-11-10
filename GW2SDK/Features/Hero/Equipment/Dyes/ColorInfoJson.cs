using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class ColorInfoJson
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
            if (member.Name == brightness.Name)
            {
                brightness = member;
            }
            else if (member.Name == contrast.Name)
            {
                contrast = member;
            }
            else if (member.Name == hue.Name)
            {
                hue = member;
            }
            else if (member.Name == saturation.Name)
            {
                saturation = member;
            }
            else if (member.Name == lightness.Name)
            {
                lightness = member;
            }
            else if (member.Name == rgb.Name)
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
            Brightness = brightness.Map(value => value.GetInt32()),
            Contrast = contrast.Map(value => value.GetDouble()),
            Hue = hue.Map(value => value.GetInt32()),
            Saturation = saturation.Map(value => value.GetDouble()),
            Lightness = lightness.Map(value => value.GetDouble()),
            Rgb = rgb.Map(value => value.GetColor(missingMemberBehavior))
        };
    }
}
