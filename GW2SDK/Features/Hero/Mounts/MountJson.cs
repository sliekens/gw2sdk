using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Mounts;

internal static class MountJson
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == defaultSkin.Name)
            {
                defaultSkin = member;
            }
            else if (member.Name == skins.Name)
            {
                skins = member;
            }
            else if (member.Name == skills.Name)
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
            Id = id.Map(value => value.GetMountName(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            DefaultSkin = defaultSkin.Map(value => value.GetInt32()),
            Skins = skins.Map(values => values.GetList(value => value.GetInt32())),
            Skills = skills.Map(
                values => values.GetList(value => value.GetSkillReference(missingMemberBehavior))
            )
        };
    }
}
