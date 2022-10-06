using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.BuildStorage;

[PublicAPI]
public static class BuildReader
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<Specialization> specializations = new("specializations");
        RequiredMember<SkillBar> skills = new("skills");
        RequiredMember<SkillBar> aquaticSkills = new("aquatic_skills");
        OptionalMember<PetSkillBar> pets = new("pets");
        OptionalMember<string?> legends = new("legends");
        OptionalMember<string?> aquaticLegends = new("aquatic_legends");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (member.NameEquals(aquaticSkills.Name))
            {
                aquaticSkills.Value = member.Value;
            }
            else if (member.NameEquals(pets.Name))
            {
                pets.Value = member.Value;
            }
            else if (member.NameEquals(legends.Name))
            {
                legends.Value = member.Value;
            }
            else if (member.NameEquals(aquaticLegends.Name))
            {
                aquaticLegends.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build
        {
            Name = name.GetValue(),
            Profession = profession.GetValue(missingMemberBehavior),
            Specializations =
                specializations.SelectMany(
                    value => ReadSpecialization(value, missingMemberBehavior)
                ),
            Skills = skills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
            AquaticSkills =
                aquaticSkills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
            Pets = pets.Select(value => ReadPets(value, missingMemberBehavior)),
            Legends = legends.SelectMany(value => value.GetString()),
            AquaticLegends = aquaticLegends.SelectMany(value => value.GetString())
        };
    }

    private static SkillBar ReadSkillBar(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> heal = new("heal");
        RequiredMember<int?> utilities = new("utilities");
        NullableMember<int> elite = new("elite");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(heal.Name))
            {
                heal.Value = member.Value;
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities.Value = member.Value;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillBar
        {
            Heal = heal.GetValue(),
            Utilities =
                utilities.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Elite = elite.GetValue()
        };
    }

    private static Specialization ReadSpecialization(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> id = new("id");
        RequiredMember<int?> traits = new("traits");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(traits.Name))
            {
                traits.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Specialization
        {
            Id = id.GetValue(),
            Traits = traits.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }

    private static PetSkillBar ReadPets(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int?> terrestrial = new("terrestrial");
        RequiredMember<int?> aquatic = new("aquatic");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(terrestrial.Name))
            {
                terrestrial.Value = member.Value;
            }
            else if (member.NameEquals(aquatic.Name))
            {
                aquatic.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkillBar
        {
            Terrestrial =
                terrestrial.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Aquatic = aquatic.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
