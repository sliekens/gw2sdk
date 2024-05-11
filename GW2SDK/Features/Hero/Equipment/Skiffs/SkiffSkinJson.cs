using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs;

internal static class SkiffSkinJson
{
    public static SkiffSkin GetSkiffSkin(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";

        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkiffSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            DyeSlots = dyeSlots.Map(
                static value => value.GetList(static value => value.GetDyeSlot())
            )
        };
    }
}
