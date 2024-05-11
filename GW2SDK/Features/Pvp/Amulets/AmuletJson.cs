using System.Text.Json;
using GuildWars2.Hero.Equipment;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Amulets;

internal static class AmuletJson
{
    public static Amulet GetAmulet(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember attributes = "attributes";

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
            else if (attributes.Match(member))
            {
                attributes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Amulet
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Attributes = attributes.Map(static value => value.GetAttributes())
        };
    }
}
