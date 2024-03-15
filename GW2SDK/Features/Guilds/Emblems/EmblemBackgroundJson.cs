using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class EmblemBackgroundJson
{
    public static EmblemBackground GetEmblemBackground(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember layers = "layers";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (layers.Match(member))
            {
                layers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EmblemBackground
        {
            Id = id.Map(value => value.GetInt32()),
            Layers = layers.Map(values => values.GetList(value => value.GetStringRequired()))
        };
    }
}
