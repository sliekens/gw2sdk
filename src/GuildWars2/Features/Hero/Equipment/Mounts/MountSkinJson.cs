using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal static class MountSkinJson
{
    public static MountSkin GetMountSkin(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";
        RequiredMember mountGuid = "mount_guid";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (dyeSlots.Match(member))
            {
                dyeSlots = member;
            }
            else if (mountGuid.Match(member))
            {
                mountGuid = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        Guid mountId = mountGuid.Map(static (in value) => value.GetGuid());

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new MountSkin
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots =
                dyeSlots.Map(static (in values) => values.GetList(static (in value) => value.GetDyeSlot())),
            MountId = mountId
        };
    }
}
