using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class SelectedAttributeCombinationJson
{
    public static SelectedAttributeCombination GetSelectedAttributeCombination(
        this JsonElement json
    )
    {
        RequiredMember id = "id";
        RequiredMember attributes = "attributes";
        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
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

        return new SelectedAttributeCombination
        {
            Id = id.Map(static value => value.GetInt32()),
            Attributes = attributes.Map(static value => value.GetAttributes())
        };
    }
}
