using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.ItemStats;

[PublicAPI]
public static class ItemStatJson
{
    public static ItemStat GetItemStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
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
            else if (member.NameEquals(attributes.Name))
            {
                attributes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemStat
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Attributes =
                attributes.SelectMany(value => value.GetItemStatAttribute(missingMemberBehavior))
        };
    }
}
