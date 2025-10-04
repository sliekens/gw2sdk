using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class BuildJson
{
    public static Build GetBuild(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember profession = "profession";
        RequiredMember specializations = "specializations";
        RequiredMember skills = "skills";
        RequiredMember aquaticSkills = "aquatic_skills";
        OptionalMember pets = "pets";
        NullableMember legends = "legends";
        NullableMember aquaticLegends = "aquatic_legends";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (profession.Match(member))
            {
                profession = member;
            }
            else if (specializations.Match(member))
            {
                specializations = member;
            }
            else if (skills.Match(member))
            {
                skills = member;
            }
            else if (aquaticSkills.Match(member))
            {
                aquaticSkills = member;
            }
            else if (pets.Match(member))
            {
                pets = member;
            }
            else if (legends.Match(member))
            {
                legends = member;
            }
            else if (aquaticLegends.Match(member))
            {
                aquaticLegends = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        (SelectedSpecialization? specialization1, SelectedSpecialization? specialization2, SelectedSpecialization? specialization3) =
            specializations.Map(static (in values) => values.GetSelectedSpecializations());
        (string? LegendId1, string? LegendId2)? legendIds = legends.Map((in values) =>
            values.GetLegendIds(specialization1, specialization2, specialization3)
        );
        (string? LegendId1, string? LegendId2)? aquaticLegendIds = aquaticLegends.Map((in values) =>
            values.GetLegendIds(specialization1, specialization2, specialization3)
        );
        return new Build
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Profession = profession.Map(static (in value) => value.GetEnum<ProfessionName>()),
            Specialization1 = specialization1,
            Specialization2 = specialization2,
            Specialization3 = specialization3,
            Skills = skills.Map(static (in value) => value.GetSkillBar()),
            AquaticSkills = aquaticSkills.Map(static (in value) => value.GetSkillBar()),
            Pets = pets.Map(static (in value) => value.GetSelectedPets()),
            Legends = (legendIds, aquaticLegendIds) switch
            {
                (not null, not null) => new SelectedLegends
                {
                    Terrestrial1 = legendIds.Value.LegendId1,
                    Terrestrial2 = legendIds.Value.LegendId2,
                    Aquatic1 = aquaticLegendIds.Value.LegendId1,
                    Aquatic2 = aquaticLegendIds.Value.LegendId2
                },
                _ => null
            }
        };
    }
}
