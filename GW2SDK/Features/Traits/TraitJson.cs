using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TraitJson
{
    public static Trait GetTrait(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember tier = new("tier");
        RequiredMember order = new("order");
        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember slot = new("slot");
        OptionalMember facts = new("facts");
        OptionalMember traitedFacts = new("traited_facts");
        OptionalMember skills = new("skills");
        RequiredMember specialization = new("specialization");
        RequiredMember icon = new("icon");

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
            Id = id.Select(value => value.GetInt32()),
            Tier = tier.Select(value => value.GetInt32()),
            Order = order.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Slot = slot.Select(value => value.GetEnum<TraitSlot>(missingMemberBehavior)),
            Icon = icon.Select(value => value.GetStringRequired()),
            SpezializationId = specialization.Select(value => value.GetInt32()),
            Facts = facts.SelectMany(
                value => value.GetTraitFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetCompoundTraitFact(missingMemberBehavior)),
            Skills = skills.SelectMany(value => value.GetTraitSkill(missingMemberBehavior))
        };
    }
}
