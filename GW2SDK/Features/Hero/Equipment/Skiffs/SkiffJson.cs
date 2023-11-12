using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs;

internal static class SkiffJson
{
    public static Skiff GetSkiff(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember dyeSlots = "dye_slots";

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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skiff
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            DyeSlots = dyeSlots.Map(
                value => value.GetList(item => item.GetDyeSlot(missingMemberBehavior))
            )
        };
    }
}
