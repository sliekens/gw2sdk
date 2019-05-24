using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Infrastructure
{
    public sealed class DiscriminatedJsonConverter : JsonConverter
    {
        private readonly DiscriminatorOptions _discriminatorOptions;

        public DiscriminatedJsonConverter(Type concreteDiscriminatorOptionsType)
            : this((DiscriminatorOptions) Activator.CreateInstance(concreteDiscriminatorOptionsType))
        {
        }

        public DiscriminatedJsonConverter([NotNull] DiscriminatorOptions discriminatorOptions)
        {
            _discriminatorOptions = discriminatorOptions ?? throw new ArgumentNullException(nameof(discriminatorOptions));
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
            var discriminator = json.Property(_discriminatorOptions.Discriminator);
            if (!_discriminatorOptions.SerializeDiscriminator)
            {
                discriminator.Remove();
            }

            var jsonTypeName = discriminator.Value.ToString();
            foreach (var typeInfo in _discriminatorOptions.GetTypes())
            {
                var (typeName, type) = typeInfo;
                if (jsonTypeName == typeName)
                {
                    return ReadJsonImpl(json.CreateReader(), type, serializer);
                }
            }

            return ReadJsonImpl(json.CreateReader(), _discriminatorOptions.FallbackType, serializer);
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
