using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TraitJson
{
    public static Trait GetTrait(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember tier = "tier";
        RequiredMember order = "order";
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember slot = "slot";
        OptionalMember facts = "facts";
        OptionalMember traitedFacts = "traited_facts";
        OptionalMember skills = "skills";
        RequiredMember specialization = "specialization";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(tier.Name))
            {
                tier = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = member;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = member;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = member;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = member;
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Trait
        {
            Id = id.Map(value => value.GetInt32()),
            Tier = tier.Map(value => value.GetInt32()),
            Order = order.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Slot = slot.Map(value => value.GetEnum<TraitSlot>(missingMemberBehavior)),
            Icon = icon.Map(value => value.GetStringRequired()),
            SpezializationId = specialization.Map(value => value.GetInt32()),
            Facts = facts.Map(
                values =>
                    values.GetList(value => value.GetTraitFact(missingMemberBehavior, out _, out _))
            ),
            TraitedFacts =
                traitedFacts.Map(
                    values => values.GetList(
                        value => value.GetCompoundTraitFact(missingMemberBehavior)
                    )
                ),
            Skills = skills.Map(
                values => values.GetList(value => value.GetTraitSkill(missingMemberBehavior))
            )
        };
    }
}
