using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Amulets;

[PublicAPI]
public static class AmuletJson
{
    public static Amulet GetAmulet(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember icon = new("icon");
        RequiredMember attributes = new("attributes");

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
            else if (member.NameEquals(attributes.Name))
            {
                attributes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Amulet
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Attributes = attributes.Select(value => value.GetAttributes(missingMemberBehavior))
        };
    }
}
