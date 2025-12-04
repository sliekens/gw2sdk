using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Decorations;

internal static class DecorationCategoryJson
{
    public static DecorationCategory GetDecorationCategory(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";

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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new DecorationCategory
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired())
        };
    }
}
