using System.Drawing;
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
        RequiredMember<int> brightness = new("brightness");
        RequiredMember<double> contrast = new("contrast");
        RequiredMember<int> hue = new("hue");
        RequiredMember<double> saturation = new("saturation");
        RequiredMember<double> lightness = new("lightness");
        RequiredMember<Color> rgb = new("rgb");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(brightness.Name))
            {
                brightness.Value = member.Value;
            }
            else if (member.NameEquals(contrast.Name))
            {
                contrast.Value = member.Value;
            }
            else if (member.NameEquals(hue.Name))
            {
                hue.Value = member.Value;
            }
            else if (member.NameEquals(saturation.Name))
            {
                saturation.Value = member.Value;
            }
            else if (member.NameEquals(lightness.Name))
            {
                lightness.Value = member.Value;
            }
            else if (member.NameEquals(rgb.Name))
            {
                rgb.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ColorInfo
        {
            Brightness = brightness.GetValue(),
            Contrast = contrast.GetValue(),
            Hue = hue.GetValue(),
            Saturation = saturation.GetValue(),
            Lightness = lightness.GetValue(),
            Rgb = rgb.Select(value => value.GetColor())
        };
    }
}
