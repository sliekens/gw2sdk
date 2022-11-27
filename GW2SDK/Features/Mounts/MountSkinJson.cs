using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
public static class MountSkinJson
{
    public static MountSkin GetMountSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(dyeSlots.Name))
            {
                dyeSlots.Value = member.Value;
            }
            else if (member.NameEquals(mount.Name))
            {
                mount.Value = member.Value;
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
            DyeSlots = dyeSlots.SelectMany(value => value.GetDyeSlot(missingMemberBehavior)),
            Mount = mount.Select(value => value.GetMountName(missingMemberBehavior))
        };
    }
}
