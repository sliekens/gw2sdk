using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public interface IJsonMapping
    {
        string Name { get; }

        MappingSignificance Significance { get; }

        void Accept(IJsonMappingVisitor visitor);
    }

    public interface IJsonObjectMapping : IJsonMapping
    {
        Type ObjectType { get; }

        UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; }

        IJsonDiscriminatorMapping? Discriminator { get; }

        List<JsonPropertyMapping> Children { get; }
    }

    public interface IJsonPropertyMapping : IJsonMapping
    {
        IJsonMapping ValueMapping { get; }
        
        /// <summary>
        ///     The property or field to bind a JSON value to (one-to-one mapping), or null if the JSON property is being
        ///     deconstructed (one-to-many mapping).
        /// </summary>
        MemberInfo? Destination { get; }
    }

    public interface IJsonValueMapping : IJsonMapping
    {
        JsonValueMappingKind ValueKind { get; }

        Type ValueType { get; }

        object ConvertJsonElement(in JsonElement element, in JsonPath path);
    }

    public interface IJsonArrayMapping : IJsonMapping
    {
        Type ValueType { get; }

        IJsonMapping ValueMapping { get; }
    }

    public interface IJsonDiscriminatorMapping : IJsonMapping
    {
        Func<JsonElement, string> Selector { get; }

        Dictionary<string, IJsonObjectMapping> Mappings { get; }
    }

    public interface IJsonMappingVisitor
    {
        void VisitObject(IJsonObjectMapping mapping);

        void VisitValue(IJsonValueMapping mapping);

        void VisitProperty(IJsonPropertyMapping mapping);

        void VisitArray(IJsonArrayMapping mapping);

        void VisitDiscriminator<TObject>(IJsonDiscriminatorMapping mapping);
    }
}
