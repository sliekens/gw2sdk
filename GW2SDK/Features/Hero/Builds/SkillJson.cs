using System.Text.Json;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillJson
{
    public static Skill GetSkill(this JsonElement json)
    {
        // Unlike most models with a 'type' property, skills don't always have it
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Bundle":
                    return json.GetBundleSkill();
                case "Elite":
                    return json.GetEliteSkill();
                case "Heal":
                    return json.GetHealSkill();
                case "Monster":
                    return json.GetMonsterSkill();
                case "Pet":
                    return json.GetPetSkill();
                case "Profession":
                    return json.GetProfessionSkill();
                case "Toolbelt":
                    return json.GetToolbeltSkill();
                case "Transform":
                    return json.GetLockedSkill();
                case "Utility":
                    return json.GetUtilitySkill();
                case "Weapon":
                    return json.GetWeaponSkill();
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
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Skill
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
            SkillFlags = flags.Map(static value => value.GetSkillFlags()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired()),
            Categories =
                categories.Map(
                    static values =>
                        values.GetList(static value => value.GetEnum<SkillCategoryName>())
                )
                ?? Empty.List<Extensible<SkillCategoryName>>()
        };
    }
}
