using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SpecializationJson
{
    public static Specialization GetSpecialization(this in JsonElement json)
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

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        var backgroundString = background.Map(static (in JsonElement value) => value.GetStringRequired());
        var professionBigIconString = professionIconBig.Map(static (in JsonElement value) => value.GetString()) ?? "";
        var professionIconString = professionIcon.Map(static (in JsonElement value) => value.GetString()) ?? "";
#pragma warning disable CS0618
        return new Specialization
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Profession = profession.Map(static (in JsonElement value) => value.GetEnum<ProfessionName>()),
            Elite = elite.Map(static (in JsonElement value) => value.GetBoolean()),
            MinorTraitIds =
                minorTraits.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())),
            MajorTraitIds =
                majorTraits.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())),
            WeaponTraitId = weaponTrait.Map(static (in JsonElement value) => value.GetInt32()),
            IconHref = iconString,
            IconUrl = new Uri(iconString),
            BackgroundHref = backgroundString,
            BackgroundUrl = new Uri(backgroundString),
            ProfessionBigIconHref = professionBigIconString,
            ProfessionBigIconUrl = string.IsNullOrEmpty(professionBigIconString) ? null : new Uri(professionBigIconString),
            ProfessionIconHref = professionIconString,
            ProfessionIconUrl = string.IsNullOrEmpty(professionIconString) ? null : new Uri(professionIconString)
        };
#pragma warning restore CS0618
    }
}
