using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class BuildJson
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = "name";
        RequiredMember profession = "profession";
        RequiredMember specializations = "specializations";
        RequiredMember skills = "skills";
        RequiredMember aquaticSkills = "aquatic_skills";
        OptionalMember pets = "pets";
        NullableMember legends = "legends";
        NullableMember aquaticLegends = "aquatic_legends";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == profession.Name)
            {
                profession = member;
            }
            else if (member.Name == specializations.Name)
            {
                specializations = member;
            }
            else if (member.Name == skills.Name)
            {
                skills = member;
            }
            else if (member.Name == aquaticSkills.Name)
            {
                aquaticSkills = member;
            }
            else if (member.Name == pets.Name)
            {
                pets = member;
            }
            else if (member.Name == legends.Name)
            {
                legends = member;
            }
            else if (member.Name == aquaticLegends.Name)
            {
                aquaticLegends = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var selectedSpecializations = specializations.Map(values => values.GetSelectedSpecializations(missingMemberBehavior));
        var legendIds = legends.Map(values => values.GetLegendIds(missingMemberBehavior));
        var aquaticLegendIds = aquaticLegends.Map(values => values.GetLegendIds(missingMemberBehavior));
        return new Build
        {
            Name = name.Map(value => value.GetStringRequired()),
            Profession =
                profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Specialization = selectedSpecializations.Specialization,
            Specialization2 = selectedSpecializations.Specialization2,
            Specialization3 = selectedSpecializations.Specialization3,
            Skills = skills.Map(value => value.GetSkillBar(missingMemberBehavior)),
            AquaticSkills = aquaticSkills.Map(value => value.GetSkillBar(missingMemberBehavior)),
            Pets = pets.Map(value => value.GetSelectedPets(missingMemberBehavior)),
            Legends = (legendIds, aquaticLegendIds) switch
            {
                (not null, not null) => new SelectedLegends
                {
                    Terrestrial1 = legendIds.Value.LegendId,
                    Terrestrial2 = legendIds.Value.LegendId2,
                    Aquatic1 = aquaticLegendIds.Value.LegendId,
                    Aquatic2 = aquaticLegendIds.Value.LegendId2
                },
                _ => null
            }
        };
    }
}
