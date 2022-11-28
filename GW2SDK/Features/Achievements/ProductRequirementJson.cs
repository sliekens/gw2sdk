using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class ProductRequirementJson
{
    public static ProductRequirement GetProductRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<ProductName> product = new("product");
        RequiredMember<AccessCondition> condition = new("condition");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(product.Name))
            {
                product.Value = member.Value;
            }
            else if (member.NameEquals(condition.Name))
            {
                condition.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProductRequirement
        {
            Product = product.GetValue(missingMemberBehavior),
            Condition = condition.GetValue(missingMemberBehavior)
        };
    }
}
