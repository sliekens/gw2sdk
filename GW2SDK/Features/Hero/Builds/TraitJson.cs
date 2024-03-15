using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitJson
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (tier.Match(member))
            {
                tier = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (facts.Match(member))
            {
                facts = member;
            }
            else if (traitedFacts.Match(member))
            {
                traitedFacts = member;
            }
            else if (skills.Match(member))
            {
                skills = member;
            }
            else if (specialization.Match(member))
            {
                specialization = member;
            }
            else if (icon.Match(member))
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
            IconHref = icon.Map(value => value.GetStringRequired()),
            SpezializationId = specialization.Map(value => value.GetInt32()),
            Facts = facts.Map(
                values => values.GetList(
                    value => value.GetFact(missingMemberBehavior, out _, out _)
                )
            ),
            TraitedFacts =
                traitedFacts.Map(
                    values => values.GetList(value => value.GetTraitedFact(missingMemberBehavior))
                ),
            Skills = skills.Map(
                values => values.GetList(value => value.GetSkill(missingMemberBehavior))
            )
        };
    }
}
