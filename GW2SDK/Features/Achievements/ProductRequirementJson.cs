using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

internal static class ProductRequirementJson
{
    public static ProductRequirement GetProductRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember product = "product";
        RequiredMember condition = "condition";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(product.Name))
            {
                product = member;
            }
            else if (member.NameEquals(condition.Name))
            {
                condition = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProductRequirement
        {
            Product = product.Map(value => value.GetEnum<ProductName>(missingMemberBehavior)),
            Condition = condition.Map(
                value => value.GetEnum<AccessCondition>(missingMemberBehavior)
            )
        };
    }
}
