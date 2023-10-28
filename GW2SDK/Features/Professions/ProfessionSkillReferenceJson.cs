using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

internal static class ProfessionSkillReferenceJson
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Profession"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == source.Name)
            {
                source = member;
            }
            else if (member.Name == attunement.Name)
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
            Id = id.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            Source = source.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior))
        };
    }
}
