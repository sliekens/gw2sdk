using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Specializations;

[PublicAPI]
public static class SpecializationJson
{
    public static Specialization GetSpecialization(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember profession = new("profession");
        RequiredMember elite = new("elite");
        RequiredMember minorTraits = new("minor_traits");
        RequiredMember majorTraits = new("major_traits");
        NullableMember weaponTrait = new("weapon_trait");
        RequiredMember icon = new("icon");
        RequiredMember background = new("background");
        OptionalMember professionIconBig = new("profession_icon_big");
        OptionalMember professionIcon = new("profession_icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = member;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite = member;
            }
            else if (member.NameEquals(minorTraits.Name))
            {
                minorTraits = member;
            }
            else if (member.NameEquals(majorTraits.Name))
            {
                majorTraits = member;
            }
            else if (member.NameEquals(weaponTrait.Name))
            {
                weaponTrait = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(background.Name))
            {
                background = member;
            }
            else if (member.NameEquals(professionIconBig.Name))
            {
                professionIconBig = member;
            }
            else if (member.NameEquals(professionIcon.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Profession = profession.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Elite = elite.Select(value => value.GetBoolean()),
            MinorTraits = minorTraits.SelectMany(value => value.GetInt32()),
            MajorTraits = majorTraits.SelectMany(value => value.GetInt32()),
            WeaponTrait = weaponTrait.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Background = background.Select(value => value.GetStringRequired()),
            ProfessionIconBig = professionIconBig.Select(value => value.GetString()) ?? "",
            ProfessionIcon = professionIcon.Select(value => value.GetString()) ?? ""
        };
    }
}
