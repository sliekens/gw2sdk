using System.Text.Json;

using GuildWars2.Hero.Equipment;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Amulets;

internal static class AmuletJson
{
    public static Amulet GetAmulet(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember attributes = "attributes";

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
            else if (attributes.Match(member))
            {
                attributes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Amulet
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            IconUrl = new Uri(iconString),
            Attributes = attributes.Map(static (in value) => value.GetAttributes())
        };
    }
}
