using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal static class ProfessionSkillSummaryJson
{
    public static ProfessionSkillSummary GetProfessionSkillSummary(this in JsonElement json)
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
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (source.Match(member))
            {
                source = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ProfessionSkillSummary
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<SkillSlot>()),
            Source = source.Map(static (in JsonElement value) => value.GetEnum<ProfessionName>()),
            Attunement = attunement.Map(static (in JsonElement value) => value.GetEnum<Attunement>())
        };
    }
}
