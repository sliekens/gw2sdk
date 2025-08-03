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

        string iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
#pragma warning disable CS0618
        return new Trait
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Tier = tier.Map(static (in JsonElement value) => value.GetInt32()),
            Order = order.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<TraitSlot>()),
            IconHref = iconString,
            IconUrl = new Uri(iconString),
            SpezializationId = specialization.Map(static (in JsonElement value) => value.GetInt32()),
            Facts =
                facts.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetFact(out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetTraitedFact())
                ),
            Skills = skills.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetSkill()))
        };
#pragma warning restore CS0618
    }
}
