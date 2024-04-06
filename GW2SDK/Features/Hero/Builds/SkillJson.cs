using System.Text.Json;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillJson
{
    public static Skill GetSkill(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Unlike most models with a 'type' property, skills don't always have it
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Bundle":
                    return json.GetBundleSkill(missingMemberBehavior);
                case "Elite":
                    return json.GetEliteSkill(missingMemberBehavior);
                case "Heal":
                    return json.GetHealSkill(missingMemberBehavior);
                case "Monster":
                    return json.GetMonsterSkill(missingMemberBehavior);
                case "Pet":
                    return json.GetPetSkill(missingMemberBehavior);
                case "Profession":
                    return json.GetProfessionSkill(missingMemberBehavior);
                case "Toolbelt":
                    return json.GetToolbeltSkill(missingMemberBehavior);
                case "Transform":
                    return json.GetLockedSkill(missingMemberBehavior);
                case "Utility":
                    return json.GetUtilitySkill(missingMemberBehavior);
                case "Weapon":
                    return json.GetWeaponSkill(missingMemberBehavior);
            }
        }

        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember facts = "facts";
        OptionalMember traitedFacts = "traited_facts";
        RequiredMember description = "description";
        OptionalMember icon = "icon";
        RequiredMember flags = "flags";
        RequiredMember chatLink = "chat_link";
        OptionalMember categories = "categories";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (flags.Match(member))
            {
                flags = member;
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skill
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
            IconHref = icon.Map(value => value.GetString()) ?? "",
            SkillFlags = flags.Map(value => value.GetSkillFlags()),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Categories = categories.Map(
                    values => values.GetList(value => value.GetEnum<SkillCategoryName>())
                )
                ?? Empty.List<Extensible<SkillCategoryName>>()
        };
    }
}
