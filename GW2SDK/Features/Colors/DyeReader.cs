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
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(baseRgb.Name))
            {
                baseRgb = baseRgb.From(member.Value);
            }
            else if (member.NameEquals(cloth.Name))
            {
                cloth = cloth.From(member.Value);
            }
            else if (member.NameEquals(leather.Name))
            {
                leather = leather.From(member.Value);
            }
            else if (member.NameEquals(metal.Name))
            {
                metal = metal.From(member.Value);
            }
            else if (member.NameEquals(fur.Name))
            {
                fur = fur.From(member.Value);
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = itemId.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
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
            BaseRgb = baseRgb.Select(value => ReadRgb(value, missingMemberBehavior)),
            Cloth = cloth.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Leather = leather.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Metal = metal.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Fur = fur.Select(value => ReadColorInfo(value, missingMemberBehavior)),
            Item = itemId.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static Color ReadRgb(JsonElement value, MissingMemberBehavior missingMemberBehavior)
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
                brightness = brightness.From(member.Value);
            }
            else if (member.NameEquals(contrast.Name))
            {
                contrast = contrast.From(member.Value);
            }
            else if (member.NameEquals(hue.Name))
            {
                hue = hue.From(member.Value);
            }
            else if (member.NameEquals(saturation.Name))
            {
                saturation = saturation.From(member.Value);
            }
            else if (member.NameEquals(lightness.Name))
            {
                lightness = lightness.From(member.Value);
            }
            else if (member.NameEquals(rgb.Name))
            {
                rgb = rgb.From(member.Value);
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
            Rgb = rgb.Select(value => ReadRgb(value, missingMemberBehavior))
        };
    }
}
