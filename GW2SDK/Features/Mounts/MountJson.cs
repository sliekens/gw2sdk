using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mounts;

[PublicAPI]
public static class MountJson
{
    public static Mount GetMount(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<MountName> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> defaultSkin = new("default_skin");
        RequiredMember<int> skins = new("skins");
        RequiredMember<SkillReference> skills = new("skills");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(defaultSkin.Name))
            {
                defaultSkin.Value = member.Value;
            }
            else if (member.NameEquals(skins.Name))
            {
                skins.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mount
        {
            Id = id.Select(value => value.GetMountName(missingMemberBehavior)),
            Name = name.GetValue(),
            DefaultSkin = defaultSkin.GetValue(),
            Skins = skins.SelectMany(value => value.GetInt32()),
            Skills = skills.SelectMany(value => value.GetSkillReference(missingMemberBehavior))
        };
    }
}
