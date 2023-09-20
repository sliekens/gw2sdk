using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Emblems;

[PublicAPI]
public static class EmblemJson
{
    public static Emblem GetEmblem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> layers = new("layers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(layers.Name))
            {
                layers.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emblem
        {
            Id = id.GetValue(),
            Layers = layers.SelectMany(value => value.GetStringRequired())
        };
    }
}
