using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class EmblemForegroundJson
{
    public static EmblemForeground GetEmblemForeground(
        this JsonElement json
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EmblemForeground
        {
            Id = id.Map(static value => value.GetInt32()),
            Layers = layers.Map(static values => values.GetList(static value => value.GetStringRequired()))
        };
    }
}
