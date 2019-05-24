using System;
using System.Collections.Generic;

namespace GW2SDK.Infrastructure
{
    /// <summary>
    ///     Extend this class to configure a type with a discriminator field.
    /// </summary>
    public abstract class DiscriminatorOptions
    {
        /// <summary>Gets the base type, which is typically (but not necessarily) abstract.</summary>
        public abstract Type BaseType { get; }

        /// <summary>Gets the name of the discriminator field.</summary>
        public abstract string Discriminator { get; }

        /// <summary>Gets the CLR type that will be used if no suitable type is found.</summary>
        public abstract Type FallbackType { get; }

        /// <summary>Returns true if the discriminator should be serialized to the CLR type; otherwise false.</summary>
        public abstract bool SerializeDiscriminator { get; }

        /// <summary>Gets the mappings from discriminator values to CLR types.</summary>
        public abstract IEnumerable<(string TypeName, Type Type)> GetTypes();

        /// <summary>Creates an object which will then be populated by the serializer.</summary>
        public abstract object Create(Type objectType);
    }
}
