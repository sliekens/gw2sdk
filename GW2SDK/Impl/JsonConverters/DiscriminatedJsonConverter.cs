using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Impl.JsonConverters
{
    internal sealed class DiscriminatedJsonConverter : JsonConverter
    {
        private readonly DiscriminatorOptions _discriminatorOptions;

        internal DiscriminatedJsonConverter(Type concreteDiscriminatorOptionsType)
            : this((DiscriminatorOptions) Activator.CreateInstance(concreteDiscriminatorOptionsType))
        {
        }

        internal DiscriminatedJsonConverter(DiscriminatorOptions discriminatorOptions)
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

            Type creationType = objectType;
            var json = JObject.Load(reader);
            var discriminatorField = json.Property(_discriminatorOptions.DiscriminatorFieldName);
            if (discriminatorField is null)
            {
                // BUG: this should throw for MissingMemberHandling.Error, but API bugs would cause tests to fail
                // Uncomment when related issue is fixed: https://github.com/arenanet/api-cdi/issues/670
                //if (serializer.MissingMemberHandling == MissingMemberHandling.Error)
                //{
                //    throw new JsonSerializationException($"Discriminator field '{_discriminatorOptions.DiscriminatorFieldName}' was not found.");
                //}

                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Warning)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Warning,
                        $"Could not find discriminator field '{_discriminatorOptions.DiscriminatorFieldName}'. Continuing anyway with Type '{objectType}'.",
                        null);
                }
            }
            else
            {
                var discriminatorFieldValue = discriminatorField.Value.ToString();
                if (!_discriminatorOptions.SerializeDiscriminator)
                {
                    // Remove the discriminator field from the JSON for two possible reasons:
                    // 1. the user doesn't want to copy the discriminator value from JSON to the CLR object, only the other way around
                    // 2. the CLR object doesn't even have a discriminator property, in which case MissingMemberHandling.Error would throw
                    discriminatorField.Remove();
                }

                _discriminatorOptions.Preprocess(discriminatorFieldValue, json);

                if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Info)
                {
                    serializer.TraceWriter.Trace(TraceLevel.Info,
                        $"Found discriminator field '{discriminatorField.Name}' with value '{discriminatorFieldValue}'.",
                        null);
                }

                var found = _discriminatorOptions.GetDiscriminatedTypes().FirstOrDefault(tuple => tuple.TypeName == discriminatorFieldValue).Type;
                if (found is Type)
                {
                    creationType = found;
                    if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Info)
                    {
                        serializer.TraceWriter.Trace(TraceLevel.Info,
                            $"Discriminator value '{discriminatorFieldValue}' was used to select Type '{found}'.",
                            null);
                    }
                }
                else
                {
                    if (serializer.MissingMemberHandling == MissingMemberHandling.Error)
                    {
                        throw new JsonSerializationException($"Discriminator value '{discriminatorFieldValue}' has no corresponding Type.");
                    }

                    if (serializer.TraceWriter?.LevelFilter >= TraceLevel.Warning)
                    {
                        serializer.TraceWriter.Trace(TraceLevel.Warning,
                            $"Discriminator value '{discriminatorFieldValue}' has no corresponding Type. Continuing anyway with Type '{objectType}'.",
                            null);
                    }
                }
            }

            // There might be a different converter on the 'found' type
            // Use Deserialize to let Json.NET choose the next converter
            // Use Populate to ignore any remaining converters (prevents infinite recursion)
            if (creationType != objectType && TypeHasConverter(serializer, creationType))
            {
                return serializer.Deserialize(json.CreateReader(), creationType);
            }

            var value = _discriminatorOptions.CreateInstance(creationType);
            serializer.Populate(json.CreateReader(), value);
            return value;
        }

        private static bool TypeHasConverter(JsonSerializer serializer, Type creationType) =>
            serializer.Converters.Any(converter => converter.CanConvert(creationType))
            || creationType.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(JsonConverterAttribute));

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
            throw new NotSupportedException("DiscriminatedJsonConverter should only be used while deserializing.");
    }
}
