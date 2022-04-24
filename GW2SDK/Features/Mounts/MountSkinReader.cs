using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
public static class MountSkinReader
{
    public static MountSkin Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> icon = new("icon");
        RequiredMember<DyeSlot> dyeSlots = new("dye_slots");
        RequiredMember<MountName> mount = new("mount");

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

    private static DyeSlot ReadDyeSlot(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> colorId = new("color_id");
        RequiredMember<Material> material = new("material");
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
