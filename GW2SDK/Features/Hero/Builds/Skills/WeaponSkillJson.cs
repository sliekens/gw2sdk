using System.Text.Json;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Skills;

internal static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(this JsonElement json)
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

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Weapon"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var professionRestrictions = professions.Map(
            static values => values.GetList(static value => value.GetEnum<ProfessionName>())
        );
        return new WeaponSkill
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Facts =
                facts.Map(
                    static values => values.GetList(static value => value.GetFact(out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(
                    static values => values.GetList(static value => value.GetTraitedFact())
                ),
            Description = description.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetString()) ?? "",
            WeaponType = weaponType.Map(static value => value.GetWeaponType()),
            Professions =
                professionRestrictions.Count > 0
                    ? professionRestrictions
                    : Profession.AllProfessions,
            Attunement = attunement.Map(static value => value.GetEnum<Attunement>()),
            DualAttunement = dualAttunement.Map(static value => value.GetEnum<Attunement>()),
            Slot = slot.Map(static value => value.GetEnum<SkillSlot>()),
            FlipSkillId = flipSkill.Map(static value => value.GetInt32()),
            NextSkillId = nextChain.Map(static value => value.GetInt32()),
            PreviousSkillId = prevChain.Map(static value => value.GetInt32()),
            SkillFlags = flags.Map(static value => value.GetSkillFlags()),
            SpecializationId = specialization.Map(static value => value.GetInt32()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired()),
            Categories =
                categories.Map(
                    static values =>
                        values.GetList(static value => value.GetEnum<SkillCategoryName>())
                )
                ?? Empty.List<Extensible<SkillCategoryName>>(),
            Cost = cost.Map(static value => value.GetInt32()),
            Offhand = offhand.Map(static value => value.GetEnum<Offhand>()),
            Initiative = initiative.Map(static value => value.GetInt32())
        };
    }
}
