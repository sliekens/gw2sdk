using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mounts;

[PublicAPI]
public static class MountSkinJson
{
    public static MountSkin GetMountSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember icon = new("icon");
        RequiredMember dyeSlots = new("dye_slots");
        RequiredMember mount = new("mount");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(dyeSlots.Name))
            {
                dyeSlots = member;
            }
            else if (member.NameEquals(mount.Name))
            {
                mount = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MountSkin
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            DyeSlots = dyeSlots.SelectMany(value => value.GetDyeSlot(missingMemberBehavior)),
            Mount = mount.Select(value => value.GetMountName(missingMemberBehavior))
        };
    }
}
