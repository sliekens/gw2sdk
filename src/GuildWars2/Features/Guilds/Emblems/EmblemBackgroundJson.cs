using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class EmblemBackgroundJson
{
    public static EmblemBackground GetEmblemBackground(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember layers = "layers";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new EmblemBackground
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            LayerUrls = layers.Map(static (in values) =>
                values.GetList(static (in value) => new Uri(value.GetStringRequired()))
            )
        };
    }
}
