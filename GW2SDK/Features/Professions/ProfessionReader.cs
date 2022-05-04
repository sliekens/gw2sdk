using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public static class ProfessionReader
{
    public static Profession GetProfession(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<ProfessionName> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> code = new("code");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> iconBig = new("icon_big");
        RequiredMember<int> specializations = new("specializations");
        RequiredMember<IDictionary<string, WeaponProficiency>> weapons = new("weapons");
        RequiredMember<ProfessionFlag> flags = new("flags");
        RequiredMember<SkillReference> skills = new("skills");
        RequiredMember<Training> training = new("training");
        RequiredMember<Dictionary<int, int>> skillsByPalette = new("skills_by_palette");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(code.Name))
            {
                code.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(iconBig.Name))
            {
                iconBig.Value = member.Value;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations.Value = member.Value;
            }
            else if (member.NameEquals(weapons.Name))
            {
                weapons.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (member.NameEquals(training.Name))
            {
                training.Value = member.Value;
            }
            else if (member.NameEquals(skillsByPalette.Name))
            {
                skillsByPalette.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Profession
        {
            Id = id.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Code = code.GetValue(),
            Icon = icon.GetValue(),
            IconBig = iconBig.GetValue(),
            Specializations = specializations.SelectMany(value => value.GetInt32()),
            Weapons =
                weapons.Select(
                    value => value.GetMap(item => ReadWeapon(item, missingMemberBehavior))
                ),
            Flags = flags.GetValues(missingMemberBehavior),
            Skills = skills.SelectMany(value => ReadSkillReference(value, missingMemberBehavior)),
            Training = training.SelectMany(value => ReadTraining(value, missingMemberBehavior)),
            SkillsByPalette = skillsByPalette.Select(value => ReadMap(value))
        };
    }

    private static Dictionary<int, int> ReadMap(JsonElement json)
    {
        // The json is an iterable of key-value pairs
        // e.g.
        // [
        //   [ 1, 12343 ],
        //   [ 2, 12417 ],
        //   [ 3, 12371 ]
        // ]
        //
        // In JavaScript you could just do new Map([[1,12343],[2,12417]])
        // In C# there are no shortcuts but we can convert it to an IEnumerable<KeyValuePair>
        //  then convert that to Dictionary
        Dictionary<int, int> map = new(json.GetArrayLength());
        foreach (var member in json.EnumerateArray())
        {
            // Short-circuit invalid data
            if (member.GetArrayLength() != 2)
            {
                break;
            }

            var key = member[0].GetInt32();
            var value = member[1].GetInt32();

            map[key] = value;
        }

        return map;
    }

    private static Training ReadTraining(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<TrainingCategory> category = new("category");
        RequiredMember<string> name = new("name");
        RequiredMember<TrainingObjective> track = new("track");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(category.Name))
            {
                category.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(track.Name))
            {
                track.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Training
        {
            Id = id.GetValue(),
            Category = category.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Track = track.SelectMany(value => ReadTrainingObjective(value, missingMemberBehavior))
        };
    }

    private static TrainingObjective ReadTrainingObjective(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Skill":
                return ReadSkillObjective(json, missingMemberBehavior);
            case "Trait":
                return ReadTraitObjective(json, missingMemberBehavior);
        }

        RequiredMember<int> cost = new("cost");
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
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingObjective { Cost = cost.GetValue() };
    }

    private static SkillObjective ReadSkillObjective(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> cost = new("cost");
        RequiredMember<int> skillId = new("skill_id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Skill"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (member.NameEquals(skillId.Name))
            {
                skillId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillObjective
        {
            Cost = cost.GetValue(),
            SkillId = skillId.GetValue()
        };
    }

    private static TraitObjective ReadTraitObjective(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> cost = new("cost");
        RequiredMember<int> traitId = new("trait_id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Trait"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (member.NameEquals(traitId.Name))
            {
                traitId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitObjective
        {
            Cost = cost.GetValue(),
            TraitId = traitId.GetValue()
        };
    }

    private static SkillReference ReadSkillReference(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Profession":
                return ReadProfessionSkillReference(json, missingMemberBehavior);
            case "Heal":
                return ReadHealingSkillReference(json, missingMemberBehavior);
            case "Utility":
                return ReadUtilitySkillReference(json, missingMemberBehavior);
            case "Elite":
                return ReadEliteSkillReference(json, missingMemberBehavior);
        }

        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

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
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }

    private static HealingSkillReference ReadHealingSkillReference(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealingSkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }

    private static UtilitySkillReference ReadUtilitySkillReference(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Utility"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UtilitySkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }

    private static EliteSkillReference ReadEliteSkillReference(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Elite"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EliteSkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }

    private static ProfessionSkillReference ReadProfessionSkillReference(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");
        NullableMember<ProfessionName> source = new("source");
        NullableMember<Attunement> attunement = new("attunement");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Profession"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (member.NameEquals(source.Name))
            {
                source.Value = member.Value;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProfessionSkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            Source = source.GetValue(missingMemberBehavior),
            Attunement = attunement.GetValue(missingMemberBehavior)
        };
    }

    private static WeaponProficiency ReadWeapon(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> specialization = new("specialization");
        RequiredMember<WeaponFlag> flags = new("flags");
        RequiredMember<WeaponSkill> skills = new("skills");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(specialization.Name))
            {
                specialization.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponProficiency
        {
            RequiredSpecialization = specialization.GetValue(),
            Flags = flags.GetValues(missingMemberBehavior),
            Skills = skills.SelectMany(value => ReadWeaponSkill(value, missingMemberBehavior))
        };
    }

    private static WeaponSkill ReadWeaponSkill(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");
        NullableMember<Offhand> offhand = new("offhand");
        NullableMember<Attunement> attunement = new("attunement");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (member.NameEquals(offhand.Name))
            {
                offhand.Value = member.Value;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            Offhand = offhand.GetValue(missingMemberBehavior),
            Attunement = attunement.GetValue(missingMemberBehavior)
        };
    }
}
