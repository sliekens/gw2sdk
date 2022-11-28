using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Specializations;

[PublicAPI]
public static class SpecializationJson
{
    public static Specialization GetSpecialization(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<bool> elite = new("elite");
        RequiredMember<int> minorTraits = new("minor_traits");
        RequiredMember<int> majorTraits = new("major_traits");
        NullableMember<int> weaponTrait = new("weapon_trait");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> background = new("background");
        OptionalMember<string> professionIconBig = new("profession_icon_big");
        OptionalMember<string> professionIcon = new("profession_icon");

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
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite.Value = member.Value;
            }
            else if (member.NameEquals(minorTraits.Name))
            {
                minorTraits.Value = member.Value;
            }
            else if (member.NameEquals(majorTraits.Name))
            {
                majorTraits.Value = member.Value;
            }
            else if (member.NameEquals(weaponTrait.Name))
            {
                weaponTrait.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(background.Name))
            {
                background.Value = member.Value;
            }
            else if (member.NameEquals(professionIconBig.Name))
            {
                professionIconBig.Value = member.Value;
            }
            else if (member.NameEquals(professionIcon.Name))
            {
                professionIcon.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Specialization
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Profession = profession.GetValue(missingMemberBehavior),
            Elite = elite.GetValue(),
            MinorTraits = minorTraits.SelectMany(value => value.GetInt32()),
            MajorTraits = majorTraits.SelectMany(value => value.GetInt32()),
            WeaponTrait = weaponTrait.GetValue(),
            Icon = icon.GetValue(),
            Background = background.GetValue(),
            ProfessionIconBig = professionIconBig.GetValueOrEmpty(),
            ProfessionIcon = professionIcon.GetValueOrEmpty()
        };
    }
}
