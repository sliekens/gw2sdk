using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TraitJson
{
    public static Trait GetTrait(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> tier = new("tier");
        RequiredMember<int> order = new("order");
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<TraitSlot> slot = new("slot");
        OptionalMember<TraitFact> facts = new("facts");
        OptionalMember<CompoundTraitFact> traitedFacts = new("traited_facts");
        OptionalMember<TraitSkill> skills = new("skills");
        RequiredMember<int> specialization = new("specialization");
        RequiredMember<string> icon = new("icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(tier.Name))
            {
                tier.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts.Value = member.Value;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Trait
        {
            Id = id.GetValue(),
            Tier = tier.GetValue(),
            Order = order.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Slot = slot.GetValue(missingMemberBehavior),
            Icon = icon.GetValue(),
            SpezializationId = specialization.GetValue(),
            Facts = facts.SelectMany(
                value => value.GetTraitFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetCompoundTraitFact(missingMemberBehavior)),
            Skills = skills.SelectMany(value => value.GetTraitSkill(missingMemberBehavior))
        };
    }
}
