using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Impl.JsonConverters
{
    /// <summary>
    ///     Extend this class to configure a type with a discriminator field.
    /// </summary>
    internal abstract class DiscriminatorOptions
    {
        /// <summary>Gets the base type, which is typically (but not necessarily) abstract.</summary>
        internal abstract Type BaseType { get; }

        /// <summary>Gets the name of the discriminator field.</summary>
        internal abstract string DiscriminatorFieldName { get; }

        /// <summary>Returns true if the discriminator should be serialized to the CLR type; otherwise false.</summary>
        internal abstract bool SerializeDiscriminator { get; }

        /// <summary>Callback that can optionally mutate the JObject before it is converted.</summary>
        internal virtual void Preprocess(string discriminator, JObject jsonObject)
        {
        }

        /// <summary>Callback that creates an object which will then be populated by the serializer.</summary>
        internal virtual object CreateInstance(Type discriminatedType) => Activator.CreateInstance(discriminatedType);

        /// <summary>Gets the mappings from discriminator values to CLR types.</summary>
        internal abstract IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes();
    }
}
