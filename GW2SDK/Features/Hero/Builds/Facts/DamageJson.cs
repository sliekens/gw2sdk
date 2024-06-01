using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class DamageJson
{
    public static Damage GetDamage(
        this JsonElement json,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        RequiredMember hitCount = "hit_count";
        RequiredMember damageMultiplier = "dmg_multiplier";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Damage"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (text.Match(member))
            {
                text = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (hitCount.Match(member))
            {
                hitCount = member;
            }
            else if (damageMultiplier.Match(member))
            {
                damageMultiplier = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Damage
        {
            Text = text.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            HitCount = hitCount.Map(static value => value.GetInt32()),
            DamageMultiplier = damageMultiplier.Map(static value => value.GetDouble())
        };
    }
}
