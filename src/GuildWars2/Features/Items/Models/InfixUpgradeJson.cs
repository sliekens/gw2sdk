using System.Text.Json;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static ImmutableValueDictionary<Extensible<AttributeName>, int> GetAttributes(
        this in JsonElement json
    )
    {
        ImmutableDictionary<Extensible<AttributeName>, int>.Builder builder =
            ImmutableDictionary.CreateBuilder<Extensible<AttributeName>, int>();
        foreach (JsonElement entry in json.EnumerateArray())
        {
            RequiredMember attribute = "attribute";
            RequiredMember modifier = "modifier";
            foreach (JsonProperty member in entry.EnumerateObject())
            {
                if (attribute.Match(member))
                {
                    attribute = member;
                }
                else if (modifier.Match(member))
                {
                    modifier = member;
                }
                else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedMember(member.Name);
                }
            }

            Extensible<AttributeName> key = attribute.Map(static (in value) => value.GetAttributeName());
            int value = modifier.Map(static (in value) => value.GetInt32());
            builder.Add(key, value);
        }

        return new ImmutableValueDictionary<Extensible<AttributeName>, int>(builder.ToImmutable());
    }
}
