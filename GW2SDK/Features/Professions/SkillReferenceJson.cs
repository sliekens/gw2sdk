using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class SkillReferenceJson
{
    public static SkillReference GetSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Profession":
                return json.GetProfessionSkillReference(missingMemberBehavior);
            case "Heal":
                return json.GetHealingSkillReference(missingMemberBehavior);
            case "Utility":
                return json.GetUtilitySkillReference(missingMemberBehavior);
            case "Elite":
                return json.GetEliteSkillReference(missingMemberBehavior);
        }

        RequiredMember id = new("id");
        RequiredMember slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.Select(value => value.GetInt32()),
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior))
        };
    }
}
