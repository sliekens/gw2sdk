using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SubskillJson
{
    public static Subskill GetSubskill(this in JsonElement json)
    {
        RequiredMember id = "id";
        NullableMember attunement = "attunement";
        NullableMember form = "form";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (form.Match(member))
            {
                form = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Subskill
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Attunement = attunement.Map(static (in JsonElement value) => value.GetEnum<Attunement>()),
            Form = form.Map(static (in JsonElement value) => value.GetEnum<Transformation>())
        };
    }
}
