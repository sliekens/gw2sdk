using System;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Colors;

[PublicAPI]
public static class DyeReader
{
    public static Dye GetDye(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<Color> baseRgb = new("base_rgb");
        RequiredMember<ColorInfo> cloth = new("cloth");
        RequiredMember<ColorInfo> leather = new("leather");
        RequiredMember<ColorInfo> metal = new("metal");
        OptionalMember<ColorInfo> fur = new("fur");
        NullableMember<int> itemId = new("item");
        RequiredMember<ColorCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(baseRgb.Name))
            {
                baseRgb.Value = member.Value;
            }
            else if (member.NameEquals(cloth.Name))
            {
                cloth.Value = member.Value;
            }
            else if (member.NameEquals(leather.Name))
            {
                leather.Value = member.Value;
            }
            else if (member.NameEquals(metal.Name))
            {
                metal.Value = member.Value;
            }
            else if (member.NameEquals(fur.Name))
            {
                fur.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dye
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            BaseRgb = baseRgb.Select(value => ReadRgb(value)),
            Cloth = cloth.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Leather = leather.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Metal = metal.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Fur = fur.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Item = itemId.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static Color ReadRgb(JsonElement value)
    {
        var red = value[0].GetInt32();
        var green = value[1].GetInt32();
        var blue = value[2].GetInt32();
        return Color.FromArgb(red, green, blue);
    }

    private static ColorInfo ReadColorInfo(
        JsonElement json,
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
            Rgb = rgb.Select(value => ReadRgb(value))
        };
    }
}
