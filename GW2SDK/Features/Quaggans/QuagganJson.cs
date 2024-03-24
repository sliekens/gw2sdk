using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quaggans;

internal static class QuagganJson
{
    public static Quaggan GetQuaggan(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember url = "url";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (url.Match(member))
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
            Id = id.Map(value => value.GetStringRequired()),
            ImageHref = url.Map(value => value.GetStringRequired())
        };
    }
}
