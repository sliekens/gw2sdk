using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mounts;

internal static class MountSkinJson
{
    public static MountSkin GetMountSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";
        RequiredMember mount = "mount";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == dyeSlots.Name)
            {
                dyeSlots = member;
            }
            else if (member.Name == mount.Name)
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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            DyeSlots =
                dyeSlots.Map(
                    values => values.GetList(value => value.GetDyeSlot(missingMemberBehavior))
                ),
            Mount = mount.Map(value => value.GetMountName(missingMemberBehavior))
        };
    }
}
