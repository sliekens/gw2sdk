using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class SkillReferenceJson
{
    public static SkillReference GetSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        NullableMember attunement = new("attunement");
        NullableMember form = new("form");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = member;
            }
            else if (member.NameEquals(form.Name))
            {
                form = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.Select(value => value.GetInt32()),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Form = form.Select(value => value.GetEnum<Transformation>(missingMemberBehavior))
        };
    }
}
