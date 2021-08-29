using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed class QuagganRefReader : IJsonReader<QuagganRef>
    {
        public QuagganRef Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<string>("id");
            var url = new RequiredMember<string>("url");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(url.Name))
                {
                    url = url.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new QuagganRef
            {
                Id = id.GetValue(),
                PictureHref = url.GetValue()
            };
        }
    }
}
