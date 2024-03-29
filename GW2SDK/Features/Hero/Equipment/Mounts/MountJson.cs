using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (defaultSkin.Match(member))
            {
                defaultSkin = member;
            }
            else if (skins.Match(member))
            {
                skins = member;
            }
            else if (skills.Match(member))
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
            Id = id.Map(value => value.GetMountName()),
            Name = name.Map(value => value.GetStringRequired()),
            DefaultSkinId = defaultSkin.Map(value => value.GetInt32()),
            SkinIds = skins.Map(values => values.GetList(value => value.GetInt32())),
            Skills = skills.Map(
                values => values.GetList(value => value.GetSkillReference(missingMemberBehavior))
            )
        };
    }
}
