using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class MaterialCategoryReader : IJsonReader<MaterialCategory>
    {
        public MaterialCategory Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var items = new RequiredMember<int[]>("items");
            var order = new RequiredMember<int>("order");

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
                else if (member.NameEquals(items.Name))
                {
                    items = items.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MaterialCategory
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Items = items.Select(value => value.GetArray(item => item.GetInt32())),
                Order = order.GetValue()
            };
        }
    }
}
