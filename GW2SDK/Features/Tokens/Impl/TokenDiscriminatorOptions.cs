using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Tokens.Impl
{
    internal sealed class TokenDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(TokenInfo);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("APIKey", typeof(ApiKeyInfo));
            yield return ("Subtoken", typeof(SubtokenInfo));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(ApiKeyInfo)) return new ApiKeyInfo();
            if (discriminatedType == typeof(SubtokenInfo)) return new SubtokenInfo();
            return new TokenInfo();
        }
    }
}
