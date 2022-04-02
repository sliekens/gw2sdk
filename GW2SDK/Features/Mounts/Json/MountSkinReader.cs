using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts.Json
{
    [PublicAPI]
    public static class MountSkinReader
    {
        public static MountSkin Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var icon = new RequiredMember<string>("icon");
            var dyeSlots = new RequiredMember<DyeSlot>("dye_slots");
            var mount = new RequiredMember<MountName>("mount");

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
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(dyeSlots.Name))
                {
                    dyeSlots = dyeSlots.From(member.Value);
                }
                else if (member.NameEquals(mount.Name))
                {
                    mount = mount.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MountSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Icon = icon.GetValue(),
                DyeSlots = dyeSlots.SelectMany(value => ReadDyeSlot(value, missingMemberBehavior)),
                Mount = mount.Select(value => MountNameReader.Read(value, missingMemberBehavior))
            };
        }

        private static DyeSlot ReadDyeSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var colorId = new RequiredMember<int>("color_id");
            var material = new RequiredMember<Material>("material");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(colorId.Name))
                {
                    colorId = colorId.From(member.Value);
                }
                else if (member.NameEquals(material.Name))
                {
                    material = material.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new DyeSlot
            {
                ColorId = colorId.GetValue(),
                Material = material.GetValue(missingMemberBehavior)
            };
        }
    }
}
