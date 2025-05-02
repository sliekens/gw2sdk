using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class BuildJson
{
    public static Build GetBuild(this JsonElement json)
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

        var (Specialization1, Specialization2, Specialization3) =
            specializations.Map(static values => values.GetSelectedSpecializations());
        var legendIds = legends.Map(values =>
            values.GetLegendIds(Specialization1, Specialization2, Specialization3)
        );
        var aquaticLegendIds = aquaticLegends.Map(values =>
            values.GetLegendIds(Specialization1, Specialization2, Specialization3)
        );
        return new Build
        {
            Name = name.Map(static value => value.GetStringRequired()),
            Profession = profession.Map(static value => value.GetEnum<ProfessionName>()),
            Specialization1 = Specialization1,
            Specialization2 = Specialization2,
            Specialization3 = Specialization3,
            Skills = skills.Map(static value => value.GetSkillBar()),
            AquaticSkills = aquaticSkills.Map(static value => value.GetSkillBar()),
            Pets = pets.Map(static value => value.GetSelectedPets()),
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
