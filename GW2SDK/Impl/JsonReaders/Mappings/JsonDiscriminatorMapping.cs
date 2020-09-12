using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonDiscriminatorMapping<TObject> : JsonMapping, IJsonDiscriminatorMapping
    {
        public JsonDiscriminatorMapping(Func<JsonElement, string> selector)
        {
            Selector = selector;
        }

        public Func<JsonElement, string> Selector { get; }

        public Dictionary<string, IJsonObjectMapping> Mappings { get; } =
            new Dictionary<string, IJsonObjectMapping>();

        public void Map<TDiscriminated>(string discriminator)
            where TDiscriminated : TObject
        {
            var mapping = new JsonObjectMapping<TDiscriminated>();
            Mappings.Add(discriminator, mapping);
        }

        public void Map<TDiscriminated>(string discriminator, MapObject<TDiscriminated> map)
            where TDiscriminated : TObject
        {
            var mapping = new JsonObjectMapping<TDiscriminated>();
            map(mapping);
            Mappings.Add(discriminator, mapping);
        }

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitDiscriminator<TObject>(this);
    }
}
