using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds;

internal static class SpecializationJson
{
    public static Specialization GetSpecialization(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember profession = "profession";
        RequiredMember elite = "elite";
        RequiredMember minorTraits = "minor_traits";
        RequiredMember majorTraits = "major_traits";
        NullableMember weaponTrait = "weapon_trait";
        RequiredMember icon = "icon";
        RequiredMember background = "background";
        OptionalMember professionIconBig = "profession_icon_big";
        OptionalMember professionIcon = "profession_icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == profession.Name)
            {
                profession = member;
            }
            else if (member.Name == elite.Name)
            {
                elite = member;
            }
            else if (member.Name == minorTraits.Name)
            {
                minorTraits = member;
            }
            else if (member.Name == majorTraits.Name)
            {
                majorTraits = member;
            }
            else if (member.Name == weaponTrait.Name)
            {
                weaponTrait = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == background.Name)
            {
                background = member;
            }
            else if (member.Name == professionIconBig.Name)
            {
                professionIconBig = member;
            }
            else if (member.Name == professionIcon.Name)
            {
                professionIcon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Specialization
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Profession =
                profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Elite = elite.Map(value => value.GetBoolean()),
            MinorTraitIds = minorTraits.Map(values => values.GetList(value => value.GetInt32())),
            MajorTraitIds = majorTraits.Map(values => values.GetList(value => value.GetInt32())),
            WeaponTraitId = weaponTrait.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            BackgroundHref = background.Map(value => value.GetStringRequired()),
            ProfessionIconBig = professionIconBig.Map(value => value.GetString()) ?? "",
            ProfessionIcon = professionIcon.Map(value => value.GetString()) ?? ""
        };
    }
}
