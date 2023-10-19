﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class ProfessionSkillReferenceJson
{
    public static ProfessionSkillReference GetProfessionSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";
        NullableMember source = "source";
        NullableMember attunement = "attunement";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Profession"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = member;
            }
            else if (member.NameEquals(source.Name))
            {
                source = member;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProfessionSkillReference
        {
            Id = id.Select(value => value.GetInt32()),
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            Source = source.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior))
        };
    }
}
