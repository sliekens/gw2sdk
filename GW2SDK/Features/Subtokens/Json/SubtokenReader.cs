using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Subtokens.Json
{
    [PublicAPI]
    public static class SubtokenReader
    {
        public static CreatedSubtoken Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new CreatedSubtoken
            {
                Subtoken = subtoken.GetValue()
            };
        }
    }
}
