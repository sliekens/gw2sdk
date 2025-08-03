using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Skills;

internal static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember facts = "facts";
        OptionalMember traitedFacts = "traited_facts";
        RequiredMember description = "description";
        OptionalMember icon = "icon";
        RequiredMember weaponType = "weapon_type";
        RequiredMember professions = "professions";
        RequiredMember slot = "slot";
        NullableMember flipSkill = "flip_skill";
        NullableMember nextChain = "next_chain";
        NullableMember prevChain = "prev_chain";
        RequiredMember flags = "flags";
        NullableMember specialization = "specialization";
        RequiredMember chatLink = "chat_link";
        NullableMember attunement = "attunement";
        NullableMember dualAttunement = "dual_attunement";
        OptionalMember categories = "categories";
        NullableMember cost = "cost";
        NullableMember offhand = "dual_wield";
        NullableMember initiative = "initiative";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Weapon"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (facts.Match(member))
            {
                facts = member;
            }
            else if (traitedFacts.Match(member))
            {
                traitedFacts = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (weaponType.Match(member))
            {
                weaponType = member;
            }
            else if (professions.Match(member))
            {
                professions = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (flipSkill.Match(member))
            {
                flipSkill = member;
            }
            else if (nextChain.Match(member))
            {
                nextChain = member;
            }
            else if (prevChain.Match(member))
            {
                prevChain = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (specialization.Match(member))
            {
                specialization = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (categories.Match(member))
            {
                categories = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (dualAttunement.Match(member))
            {
                dualAttunement = member;
            }
            else if (specialization.Match(member))
            {
                specialization = member;
            }
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (offhand.Match(member))
            {
                offhand = member;
            }
            else if (initiative.Match(member))
            {
                initiative = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        ValueList<Extensible<ProfessionName>> professionRestrictions = professions.Map(static (in JsonElement values) =>
            values.GetList(static (in JsonElement value) => value.GetEnum<ProfessionName>())
        );
        var iconString = icon.Map(static (in JsonElement value) => value.GetString()) ?? "";
        return new WeaponSkill
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Facts =
                facts.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetFact(out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetTraitedFact())
                ),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            WeaponType = weaponType.Map(static (in JsonElement value) => value.GetWeaponType()),
            Professions =
                professionRestrictions.Count > 0
                    ? professionRestrictions
                    : Profession.AllProfessions,
            Attunement = attunement.Map(static (in JsonElement value) => value.GetEnum<Attunement>()),
            DualAttunement = dualAttunement.Map(static (in JsonElement value) => value.GetEnum<Attunement>()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<SkillSlot>()),
            FlipSkillId = flipSkill.Map(static (in JsonElement value) => value.GetInt32()),
            NextSkillId = nextChain.Map(static (in JsonElement value) => value.GetInt32()),
            PreviousSkillId = prevChain.Map(static (in JsonElement value) => value.GetInt32()),
            SkillFlags = flags.Map(static (in JsonElement value) => value.GetSkillFlags()),
            SpecializationId = specialization.Map(static (in JsonElement value) => value.GetInt32()),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired()),
            Categories =
                categories.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<SkillCategoryName>())
                )
                ?? new Collections.ValueList<Extensible<SkillCategoryName>>(),
            Cost = cost.Map(static (in JsonElement value) => value.GetInt32()),
            Offhand = offhand.Map(static (in JsonElement value) => value.GetEnum<Offhand>()),
            Initiative = initiative.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
