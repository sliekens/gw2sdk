using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs;

internal static class SkiffSkinJson
{
    public static SkiffSkin GetSkiffSkin(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";

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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new SkiffSkin
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots = dyeSlots.Map(static (in value) =>
                value.GetList(static (in value) => value.GetDyeSlot())
            )
        };
    }
}
