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
            Name = name.Map(static (in value) => value.GetStringRequired()), // Type or member is obsolete
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots =
                dyeSlots.Map(static (in values) => values.GetList(static (in value) => value.GetDyeSlot())),
#pragma warning disable CS0618 // Type or member is obsolete
            Mount = mountId.ToString() switch
            {
                "36e3f56f-19b4-457d-a87f-0360ca47d60b" => MountName.Griffon,
                "38644d7a-7a2d-4d65-b6f2-c9619809e38b" => MountName.Jackal,
                "b3461b6a-fbb5-4777-8c0b-a44597fc7d23" => MountName.Raptor,
                "728c84c6-2573-4496-8222-205e2d1c4b96" => MountName.RollerBeetle,
                "89609486-b26b-4d87-b98c-d0f183daa08a" => MountName.Skimmer,
                "3aeb65da-6c8c-40e9-87ba-0ceac5cb2f54" => MountName.Skyscale,
                "ced0b70c-1f72-4c16-8ab1-f16d25efa6ee" => MountName.Springer,
                "b68cfe78-4169-4c0b-ae21-4422b4523c02" => MountName.SiegeTurtle,
                "705e8b54-b84a-4d45-9448-5be4c3839df3" => MountName.Warclaw,
                _ => "Unknown"
            },
#pragma warning restore CS0618 // Type or member is obsolete
            MountId = mountId
        };
    }
}
