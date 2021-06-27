using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorReader : IColorReader
    {
        public Color Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var baseRgb = new RequiredMember<int[]>("base_rgb");
            var cloth = new RequiredMember<ColorInfo>("cloth");
            var leather = new RequiredMember<ColorInfo>("leather");
            var metal = new RequiredMember<ColorInfo>("metal");
            var fur = new OptionalMember<ColorInfo>("fur");
            var itemId = new NullableMember<int>("item");
            var categories = new RequiredMember<ColorCategoryName[]>("categories");

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

            return new Color
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                BaseRgb = baseRgb.Select(value => value.GetArray(item => item.GetInt32())),
                Cloth = cloth.Select(value => ReadColorInfo(value, missingMemberBehavior)),
                Leather = leather.Select(value => ReadColorInfo(value, missingMemberBehavior)),
                Metal = metal.Select(value => ReadColorInfo(value, missingMemberBehavior)),
                Fur = fur.Select(value => ReadColorInfo(value, missingMemberBehavior)),
                Item = itemId.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        private ColorInfo ReadColorInfo(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var brightness = new RequiredMember<int>("brightness");
            var contrast = new RequiredMember<double>("contrast");
            var hue = new RequiredMember<int>("hue");
            var saturation = new RequiredMember<double>("saturation");
            var lightness = new RequiredMember<double>("lightness");
            var rgb = new RequiredMember<int[]>("rgb");

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
                Rgb = rgb.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }
    }
}
