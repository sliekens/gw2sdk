using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (profession.Match(member))
            {
                profession = member;
            }
            else if (elite.Match(member))
            {
                elite = member;
            }
            else if (minorTraits.Match(member))
            {
                minorTraits = member;
            }
            else if (majorTraits.Match(member))
            {
                majorTraits = member;
            }
            else if (weaponTrait.Match(member))
            {
                weaponTrait = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (background.Match(member))
            {
                background = member;
            }
            else if (professionIconBig.Match(member))
            {
                professionIconBig = member;
            }
            else if (professionIcon.Match(member))
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
                profession.Map(value => value.GetEnum<ProfessionName>()),
            Elite = elite.Map(value => value.GetBoolean()),
            MinorTraitIds = minorTraits.Map(values => values.GetList(value => value.GetInt32())),
            MajorTraitIds = majorTraits.Map(values => values.GetList(value => value.GetInt32())),
            WeaponTraitId = weaponTrait.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            BackgroundHref = background.Map(value => value.GetStringRequired()),
            ProfessionBigIconHref = professionIconBig.Map(value => value.GetString()) ?? "",
            ProfessionIconHref = professionIcon.Map(value => value.GetString()) ?? ""
        };
    }
}
