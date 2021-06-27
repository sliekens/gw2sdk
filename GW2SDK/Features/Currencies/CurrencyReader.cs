using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyReader : ICurrencyReader
    {
        public Currency Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var order = new RequiredMember<int>("order");
            var icon = new RequiredMember<string>("icon");

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
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Currency
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValue(),
                Order = order.GetValue(),
                Icon = icon.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
