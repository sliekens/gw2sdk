using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Quaggans;

internal static class QuagganJson
{
    public static Quaggan GetQuaggan(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember url = "url";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (url.Match(member))
            {
                url = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string urlString = url.Map(static (in value) => value.GetStringRequired());
#pragma warning disable CS0618 // Suppress obsolete warning for ImageHref assignment
        return new Quaggan
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            ImageHref = urlString,
            ImageUrl = new Uri(urlString)
        };
#pragma warning restore CS0618
    }
}
