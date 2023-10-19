using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mounts;

[PublicAPI]
public static class MountJson
{
    public static Mount GetMount(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember defaultSkin = "default_skin";
        RequiredMember skins = "skins";
        RequiredMember skills = "skills";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(defaultSkin.Name))
            {
                defaultSkin = member;
            }
            else if (member.NameEquals(skins.Name))
            {
                skins = member;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mount
        {
            Id = id.Select(value => value.GetMountName(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            DefaultSkin = defaultSkin.Select(value => value.GetInt32()),
            Skins = skins.Select(values => values.GetList(value => value.GetInt32())),
            Skills = skills.Select(values => values.GetList(value => value.GetSkillReference(missingMemberBehavior)))
        };
    }
}
