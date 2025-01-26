using System.Text.Json;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Skills;

internal static class MonsterSkillJson
{
    public static MonsterSkill GetMonsterSkill(this JsonElement json)
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
        OptionalMember categories = "categories";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Monster"))
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var professionRestrictions = professions.Map(
            static values => values.GetList(static value => value.GetEnum<ProfessionName>())
        );
        return new MonsterSkill
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
                ?? []
        };
    }
}
