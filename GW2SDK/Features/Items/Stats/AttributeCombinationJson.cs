using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeCombinationJson
{
    public static AttributeCombination GetAttributeCombination(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AttributeCombination
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Attributes = attributes.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetAttribute())
            )
        };
    }
}
