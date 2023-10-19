using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class RechargeTraitFactJson
{
    public static RechargeTraitFact GetRechargeTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember text = new("text");
        OptionalMember icon = new("icon");
        RequiredMember recharge = new("value");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Recharge"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
            else if (member.NameEquals(text.Name))
            {
                text.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(recharge.Name))
            {
                recharge.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RechargeTraitFact
        {
            Text = text.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetString()) ?? "",
            Value = recharge.Select(value => value.GetDouble())
        };
    }
}
