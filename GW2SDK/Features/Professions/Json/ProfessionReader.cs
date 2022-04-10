﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Json
{
    [PublicAPI]
    public static class ProfessionReader
    {
        public static Profession Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<ProfessionName>("id");
            var name = new RequiredMember<string>("name");
            var code = new RequiredMember<int>("code");
            var icon = new RequiredMember<string>("icon");
            var iconBig = new RequiredMember<string>("icon_big");
            var specializations = new RequiredMember<int>("specializations");
            var weapons = new RequiredMember<IDictionary<string, WeaponProficiency>>("weapons");
            var flags = new RequiredMember<ProfessionFlag>("flags");
            var skills = new RequiredMember<SkillReference>("skills");
            var training = new RequiredMember<Training>("training");
            var skillsByPalette = new RequiredMember<Dictionary<int, int>>("skills_by_palette");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(code.Name))
                {
                    code = code.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(iconBig.Name))
                {
                    iconBig = iconBig.From(member.Value);
                }
                else if (member.NameEquals(specializations.Name))
                {
                    specializations = specializations.From(member.Value);
                }
                else if (member.NameEquals(weapons.Name))
                {
                    weapons = weapons.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(skills.Name))
                {
                    skills = skills.From(member.Value);
                }
                else if (member.NameEquals(training.Name))
                {
                    training = training.From(member.Value);
                }
                else if (member.NameEquals(skillsByPalette.Name))
                {
                    skillsByPalette = skillsByPalette.From(member.Value);
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
                Weapons = weapons.Select(value => value.GetMap(item => ReadWeapon(item, missingMemberBehavior))),
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
            var map = new Dictionary<int, int>(json.GetArrayLength());
            foreach (var member in json.EnumerateArray())
            {
                // Short-circuit invalid data
                if (member.GetArrayLength() != 2)
                {
                    break;
                }

                var key = member[0]
                    .GetInt32();
                var value = member[1]
                    .GetInt32();

                map[key] = value;
            }

            return map;
        }

        private static Training ReadTraining(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var category = new RequiredMember<TrainingCategory>("category");
            var name = new RequiredMember<string>("name");
            var track = new RequiredMember<TrainingObjective>("track");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(category.Name))
                {
                    category = category.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(track.Name))
                {
                    track = track.From(member.Value);
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
            switch (json.GetProperty("type")
                        .GetString())
            {
                case "Skill":
                    return ReadSkillObjective(json, missingMemberBehavior);
                case "Trait":
                    return ReadTraitObjective(json, missingMemberBehavior);
            }

            var cost = new RequiredMember<int>("cost");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(cost.Name))
                {
                    cost = cost.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new TrainingObjective
            {
                Cost = cost.GetValue()
            };
        }

        private static SkillObjective ReadSkillObjective(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var cost = new RequiredMember<int>("cost");
            var skillId = new RequiredMember<int>("skill_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Skill"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(cost.Name))
                {
                    cost = cost.From(member.Value);
                }
                else if (member.NameEquals(skillId.Name))
                {
                    skillId = skillId.From(member.Value);
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

        private static TraitObjective ReadTraitObjective(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var cost = new RequiredMember<int>("cost");
            var traitId = new RequiredMember<int>("trait_id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trait"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(cost.Name))
                {
                    cost = cost.From(member.Value);
                }
                else if (member.NameEquals(traitId.Name))
                {
                    traitId = traitId.From(member.Value);
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

        private static SkillReference ReadSkillReference(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type")
                        .GetString())
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

            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
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
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Heal"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
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
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Utility"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
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
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Elite"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
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
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");
            var source = new NullableMember<ProfessionName>("source");
            var attunement = new NullableMember<Attunement>("attunement");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Profession"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
                }
                else if (member.NameEquals(source.Name))
                {
                    source = source.From(member.Value);
                }
                else if (member.NameEquals(attunement.Name))
                {
                    attunement = attunement.From(member.Value);
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

        private static WeaponProficiency ReadWeapon(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var specialization = new NullableMember<int>("specialization");
            var flags = new RequiredMember<WeaponFlag>("flags");
            var skills = new RequiredMember<WeaponSkill>("skills");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(specialization.Name))
                {
                    specialization = specialization.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(skills.Name))
                {
                    skills = skills.From(member.Value);
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

        private static WeaponSkill ReadWeaponSkill(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");
            var offhand = new NullableMember<Offhand>("offhand");
            var attunement = new NullableMember<Attunement>("attunement");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
                }
                else if (member.NameEquals(offhand.Name))
                {
                    offhand = offhand.From(member.Value);
                }
                else if (member.NameEquals(attunement.Name))
                {
                    attunement = attunement.From(member.Value);
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
}