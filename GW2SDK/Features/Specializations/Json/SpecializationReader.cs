using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Specializations.Models;
using JetBrains.Annotations;

namespace GW2SDK.Specializations.Json;

[PublicAPI]
public static class SpecializationReader
{
    public static Specialization Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = profession.From(member.Value);
            }
            else if (member.NameEquals(elite.Name))
            {
                elite = elite.From(member.Value);
            }
            else if (member.NameEquals(minorTraits.Name))
            {
                minorTraits = minorTraits.From(member.Value);
            }
            else if (member.NameEquals(majorTraits.Name))
            {
                majorTraits = majorTraits.From(member.Value);
            }
            else if (member.NameEquals(weaponTrait.Name))
            {
                weaponTrait = weaponTrait.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(background.Name))
            {
                background = background.From(member.Value);
            }
            else if (member.NameEquals(professionIconBig.Name))
            {
                professionIconBig = professionIconBig.From(member.Value);
            }
            else if (member.NameEquals(professionIcon.Name))
            {
                professionIcon = professionIcon.From(member.Value);
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
