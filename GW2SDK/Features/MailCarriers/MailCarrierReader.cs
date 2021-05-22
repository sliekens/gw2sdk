using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    public sealed class MailCarrierReader : IMailCarrierReader
    {
        public MailCarrier Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var unlockItems = new RequiredMember<int[]>("unlock_items");
            var order = new RequiredMember<int>("order");
            var icon = new RequiredMember<string>("icon");
            var name = new RequiredMember<string>("name");
            var flags = new RequiredMember<MailCarrierFlag[]>("flags");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(unlockItems.Name))
                {
                    unlockItems = unlockItems.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MailCarrier
            {
                Id = id.GetValue(),
                UnlockItems = unlockItems.Select(value => value.GetArray(item => item.GetInt32())),
                Order = order.GetValue(),
                Icon = icon.GetValue(),
                Name = name.GetValue(),
                Flags = flags.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
