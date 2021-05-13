using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenReader : ISubtokenReader
    {
        public CreatedSubtoken Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var subtoken = new RequiredMember<string>("subtoken");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(subtoken.Name))
                {
                    subtoken = subtoken.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new CreatedSubtoken { Subtoken = subtoken.GetValue() };
        }
    }
}
