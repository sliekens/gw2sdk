using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class ProductRequirementJson
{
    public static ProductRequirement GetProductRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember product = new("product");
        RequiredMember condition = new("condition");

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
            Product = product.Select(value => value.GetEnum<ProductName>(missingMemberBehavior)),
            Condition = condition.Select(value => value.GetEnum<AccessCondition>(missingMemberBehavior))
        };
    }
}
