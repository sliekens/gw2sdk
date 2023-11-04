using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Skills;

internal static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember facts = "facts";
        OptionalMember traitedFacts = "traited_facts";
        RequiredMember description = "description";
        OptionalMember icon = "icon";
        NullableMember weaponType = "weapon_type";
        OptionalMember professions = "professions";
        NullableMember slot = "slot";
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Weapon"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == facts.Name)
            {
                facts = member;
            }
            else if (member.Name == traitedFacts.Name)
            {
                traitedFacts = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == weaponType.Name)
            {
                weaponType = member;
            }
            else if (member.Name == professions.Name)
            {
                professions = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == flipSkill.Name)
            {
                flipSkill = member;
            }
            else if (member.Name == nextChain.Name)
            {
                nextChain = member;
            }
            else if (member.Name == prevChain.Name)
            {
                prevChain = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == specialization.Name)
            {
                specialization = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == chatLink.Name)
            {
                chatLink = member;
            }
            else if (member.Name == categories.Name)
            {
                categories = member;
            }
            else if (member.Name == attunement.Name)
            {
                attunement = member;
            }
            else if (member.Name == dualAttunement.Name)
            {
                dualAttunement = member;
            }
            else if (member.Name == specialization.Name)
            {
                specialization = member;
            }
            else if (member.Name == cost.Name)
            {
                cost = member;
            }
            else if (member.Name == offhand.Name)
            {
                offhand = member;
            }
            else if (member.Name == initiative.Name)
            {
                initiative = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Facts =
                facts.Map(
                    values =>
                        values.GetList(value => value.GetFact(missingMemberBehavior, out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(
                    values => values.GetList(value => value.GetTraitedFact(missingMemberBehavior))
                ),
            Description = description.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetString()),
            WeaponType = weaponType.Map(value => value.GetEnum<WeaponType>(missingMemberBehavior)),
            Professions =
                professions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionName>(missingMemberBehavior)
                        )
                ),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            DualAttunement =
                dualAttunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkillId = flipSkill.Map(value => value.GetInt32()),
            NextSkillId = nextChain.Map(value => value.GetInt32()),
            PreviousSkillId = prevChain.Map(value => value.GetInt32()),
            SkillFlag =
                flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<SkillFlag>(missingMemberBehavior)
                    )
                ),
            SpecializationId = specialization.Map(value => value.GetInt32()),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Categories =
                categories.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<SkillCategoryName>(missingMemberBehavior)
                        )
                ),
            Cost = cost.Map(value => value.GetInt32()),
            Offhand = offhand.Map(value => value.GetEnum<Offhand>(missingMemberBehavior)),
            Initiative = initiative.Map(value => value.GetInt32())
        };
    }
}
