using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats.Json
{
    [PublicAPI]
    public static class ItemStatReader
    {
        public static ItemStat Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var attributes = new RequiredMember<ItemStatAttribute>("attributes");

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
                else if (member.NameEquals(attributes.Name))
                {
                    attributes = attributes.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemStat
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Attributes = attributes.SelectMany(value => ReadAttribute(value, missingMemberBehavior))
            };
        }

        private static ItemStatAttribute ReadAttribute(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var attribute = new RequiredMember<UpgradeAttributeName>("attribute");
            var multiplier = new RequiredMember<double>("multiplier");
            var value = new RequiredMember<int>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(attribute.Name))
                {
                    attribute = attribute.From(member.Value);
                }
                else if (member.NameEquals(multiplier.Name))
                {
                    multiplier = multiplier.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemStatAttribute
            {
                Attribute = attribute.GetValue(missingMemberBehavior),
                Multiplier = multiplier.GetValue(),
                Value = value.GetValue()
            };
        }
    }
}
