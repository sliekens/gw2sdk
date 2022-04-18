using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Mounts.Models;
using JetBrains.Annotations;

namespace GW2SDK.Mounts.Json;

[PublicAPI]
public static class MountReader
{
    public static Mount Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(defaultSkin.Name))
            {
                defaultSkin = defaultSkin.From(member.Value);
            }
            else if (member.NameEquals(skins.Name))
            {
                skins = skins.From(member.Value);
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = skills.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mount
        {
            Id = id.Select(value => MountNameReader.Read(value, missingMemberBehavior)),
            Name = name.GetValue(),
            DefaultSkin = defaultSkin.GetValue(),
            Skins = skins.SelectMany(value => value.GetInt32()),
            Skills = skills.SelectMany(value => ReadSkill(value, missingMemberBehavior))
        };
    }

    private static SkillReference ReadSkill(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }
}
