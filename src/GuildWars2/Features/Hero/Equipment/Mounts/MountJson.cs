using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal static class MountJson
{
    public static Mount GetMount(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember defaultSkin = "default_skin";
        RequiredMember skins = "skins";
        RequiredMember skills = "skills";
        RequiredMember guid = "guid";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (guid.Match(member))
            {
                guid = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Mount
        {
            Id = id.Map(static (in value) => value.GetMountName()),
            UId = guid.Map(static (in value) => value.GetGuid()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            DefaultSkinId = defaultSkin.Map(static (in value) => value.GetInt32()),
            SkinIds = skins.Map(static (in values) => values.GetList(static (in value) => value.GetInt32())),
            Skills = skills.Map(static (in values) =>
                values.GetList(static (in value) => value.GetSkillReference())
            )
        };
    }
}
