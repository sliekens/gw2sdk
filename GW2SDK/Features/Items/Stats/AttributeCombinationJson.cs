using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeCombinationJson
{
    public static AttributeCombination GetAttributeCombination(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
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
            else if (attributes.Match(member))
            {
                attributes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeCombination
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Attributes = attributes.Map(
                static values => values.GetList(static value => value.GetAttribute())
            )
        };
    }
}
