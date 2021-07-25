using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Specializations
{
    [PublicAPI]
    public sealed class SpecializationReader : ISpecializationReader
    {
        public Specialization Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var profession = new RequiredMember<ProfessionName>("profession");
            var elite = new RequiredMember<bool>("elite");
            var minorTraits = new RequiredMember<int[]>("minor_traits");
            var majorTraits = new RequiredMember<int[]>("major_traits");
            var weaponTrait = new NullableMember<int>("weapon_trait");
            var icon = new RequiredMember<string>("icon");
            var background = new RequiredMember<string>("background");
            var professionIconBig = new OptionalMember<string>("profession_icon_big");
            var professionIcon = new OptionalMember<string>("profession_icon");

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
                MinorTraits = minorTraits.Select(value => value.GetArray(item => item.GetInt32())),
                MajorTraits = majorTraits.Select(value => value.GetArray(item => item.GetInt32())),
                WeaponTrait = weaponTrait.GetValue(),
                Icon = icon.GetValue(),
                Background = background.GetValue(),
                ProfessionIconBig = professionIconBig.GetValueOrEmpty(),
                ProfessionIcon = professionIcon.GetValueOrEmpty()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
