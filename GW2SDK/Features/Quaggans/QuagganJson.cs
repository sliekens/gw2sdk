using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quaggans;

[PublicAPI]
public static class QuagganJson
{
    public static Quaggan GetQuaggan(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember url = new("url");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(url.Name))
            {
                url = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Quaggan
        {
            Id = id.Select(value => value.GetStringRequired()),
            PictureHref = url.Select(value => value.GetStringRequired())
        };
    }
}
