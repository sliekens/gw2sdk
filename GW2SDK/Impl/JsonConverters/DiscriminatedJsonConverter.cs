using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Impl.JsonConverters
{
    public sealed class DiscriminatedJsonConverter : JsonConverter
    {
        private readonly DiscriminatorOptions _discriminatorOptions;

        public DiscriminatedJsonConverter(Type concreteDiscriminatorOptionsType)
            : this((DiscriminatorOptions) Activator.CreateInstance(concreteDiscriminatorOptionsType))
        {
        }

        public DiscriminatedJsonConverter(DiscriminatorOptions discriminatorOptions)
        {
            _discriminatorOptions = discriminatorOptions ?? throw new ArgumentNullException(nameof(discriminatorOptions));
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType) => objectType == _discriminatorOptions.BaseType;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var json = JObject.Load(reader);
            var discriminatorField = json.Property(_discriminatorOptions.DiscriminatorFieldName);
            string discriminatorFieldValue;
            if (discriminatorField is null)
            {
                discriminatorFieldValue = "";
                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Warning)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Warning,
                        $"Could not find discriminator field '{_discriminatorOptions.DiscriminatorFieldName}'.",
                        null);
                }
            }
            else
            {
                discriminatorFieldValue = discriminatorField.Value.ToString();
                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Info)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Info,
                        $"Found discriminator field '{discriminatorField.Name}' with value '{discriminatorFieldValue}'.",
                        null);
                }
            }

            var found = _discriminatorOptions.GetDiscriminatedTypes().FirstOrDefault(tuple => tuple.TypeName == discriminatorFieldValue).Type;
            if (found == null)
            {
                found = objectType;
                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Warning)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Warning,
                        $"Discriminator value '{discriminatorFieldValue}' has no corresponding Type. Continuing anyway with Type '{objectType}'.",
                        null);
                }
            }
            else
            {
                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Warning)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Info, $"Discriminator value '{discriminatorFieldValue}' was used to select Type '{found}'.", null);
                }
            }

            _discriminatorOptions.Preprocessor?.Invoke(discriminatorFieldValue, json);
            if (discriminatorField is object && !_discriminatorOptions.SerializeDiscriminator)
            {
                // Remove the discriminator field from the JSON for two possible reasons:
                // 1. the user doesn't want to copy the discriminator value from JSON to the CLR object, only the other way around
                // 2. the CLR object doesn't even have a discriminator property, in which case MissingMemberHandling.Error would throw
                discriminatorField.Remove();
            }

            // There might be a different converter on the 'found' type
            // Use Deserialize to let Json.NET choose the next converter
            // Use Populate to ignore any remaining converters (prevents recursion when the next converter is the same as this)
            if (found != objectType
                && (found.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(JsonConverterAttribute))
                    || serializer.Converters.Any(converter => converter.CanConvert(found))))
            {
                return serializer.Deserialize(json.CreateReader(), found);
            }

            var value = _discriminatorOptions.Activator?.Invoke(found) ?? Activator.CreateInstance(found);
            serializer.Populate(json.CreateReader(), value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException("DiscriminatedJsonConverter should only be used while deserializing.");
        }
    }
}
