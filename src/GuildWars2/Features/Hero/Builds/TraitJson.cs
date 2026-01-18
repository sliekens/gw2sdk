using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class TraitJson
{
    public static Trait GetTrait(this in JsonElement json)
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

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Trait
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Tier = tier.Map(static (in value) => value.GetInt32()),
            Order = order.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            Slot = slot.Map(static (in value) => value.GetEnum<TraitSlot>()),
            IconUrl = new Uri(iconString),
            SpezializationId = specialization.Map(static (in value) => value.GetInt32()),
            Facts =
                facts.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetFact(out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetTraitedFact())
                ),
            Skills = skills.Map(static (in values) => values.GetList(static (in value) => value.GetSkill()))
        };
    }
}
