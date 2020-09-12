using System;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public delegate void MapDiscriminator<TDiscriminated>
        (JsonDiscriminatorMapping<TDiscriminated> map);

    public partial class JsonObjectMapping<TObject>
    {
        public void Discriminate
            (Func<JsonElement, string> discriminatorSelector, MapDiscriminator<TObject> map)
        {
            var mapping = new JsonDiscriminatorMapping<TObject>(discriminatorSelector);
            map(mapping);
            Discriminator = mapping;
        }
    }
}
