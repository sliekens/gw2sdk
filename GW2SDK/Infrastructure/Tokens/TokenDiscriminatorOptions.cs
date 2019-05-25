using System;
using System.Collections.Generic;
using GW2SDK.Features.Tokens;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class TokenDiscriminatorOptions : DiscriminatorOptions
    {
        public override Type BaseType => typeof(TokenInfo);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("APIKey", typeof(ApiKeyInfo));
            yield return ("Subtoken", typeof(SubtokenInfo));
        }

        public override object Create(Type objectType)
        {
            if (objectType == typeof(ApiKeyInfo))
            {
                return new ApiKeyInfo();
            }

            if (objectType == typeof(SubtokenInfo))
            {
                return new SubtokenInfo();
            }

            return new TokenInfo();
        }
    }
}
