using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class EmblemJson
{
    public static Emblem GetEmblem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember layers = "layers";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == layers.Name)
            {
                layers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emblem
        {
            Id = id.Map(value => value.GetInt32()),
            Layers = layers.Map(values => values.GetList(value => value.GetStringRequired()))
        };
    }
}
