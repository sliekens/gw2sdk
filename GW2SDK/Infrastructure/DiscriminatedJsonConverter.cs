using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Infrastructure
{
    public sealed class DiscriminatedJsonConverter : JsonConverter
    {
        private readonly DiscriminatorOptions _discriminatorOptions;

        public DiscriminatedJsonConverter([NotNull] Type concreteDiscriminatorOptionsType)
            : this((DiscriminatorOptions) Activator.CreateInstance(concreteDiscriminatorOptionsType))
        {
        }

        public DiscriminatedJsonConverter([NotNull] DiscriminatorOptions discriminatorOptions)
        {
            _discriminatorOptions =
                discriminatorOptions ?? throw new ArgumentNullException(nameof(discriminatorOptions));
        }

        public override bool CanConvert(Type objectType) => _discriminatorOptions.BaseType.IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var json = JObject.Load(reader);
            var discriminatorField = json.Property(_discriminatorOptions.DiscriminatorFieldName);
            if (discriminatorField is null)
            {
                serializer.TraceWriter?.Trace(
                    TraceLevel.Error,
                    $"Could not find discriminator field '{_discriminatorOptions.DiscriminatorFieldName}'.",
                    null);
                throw new JsonSerializationException(
                    $"Could not find discriminator field with name '{_discriminatorOptions.DiscriminatorFieldName}'.");
            }

            var discriminatorFieldValue = discriminatorField.Value.ToString();
            serializer.TraceWriter?.Trace(
                TraceLevel.Info,
                $"Found discriminator field '{discriminatorField.Name}' with value '{discriminatorFieldValue}'.",
                null);
            if (!_discriminatorOptions.SerializeDiscriminator)
            {
                // Remove the discriminator field from the JSON for two possible reasons:
                // 1. the user doesn't want to copy the discriminator value from JSON to the CLR object, only the other way around
                // 2. the CLR object doesn't even have a discriminator property, in which case MissingMemberHandling.Error would throw
                discriminatorField.Remove();
            }

            foreach (var (typeName, type) in _discriminatorOptions.GetDiscriminatedTypes())
            {
                if (discriminatorFieldValue == typeName)
                {
                    serializer.TraceWriter?.Trace(
                        TraceLevel.Info,
                        $"Discriminator value '{discriminatorFieldValue}' was used to select Type '{type}'.",
                        null);
                    return ReadJsonImpl(json.CreateReader(), type, serializer);
                }
            }

            serializer.TraceWriter?.Trace(
                TraceLevel.Warning,
                $"Discriminator value '{discriminatorFieldValue}' has no corresponding Type. Continuing anyway with Type '{objectType}'.",
                null);
            return ReadJsonImpl(json.CreateReader(), objectType, serializer);
        }

        private object ReadJsonImpl(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            var value = _discriminatorOptions.Create(objectType);
            if (value == null)
            {
                throw new JsonSerializationException("No object created.");
            }

            serializer.Populate(reader, value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("DiscriminatedJsonConverter should only be used while deserializing.");
        }
    }
}
